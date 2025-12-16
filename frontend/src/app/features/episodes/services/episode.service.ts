import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable, signal, computed } from '@angular/core';
import { catchError, of, timeout } from 'rxjs';
import { StorageService } from '@app/core/service/storage.service';
import { EpisodeResponseDTO } from '@features/episodes/models/dto/episodeResponse.dto';
import { environment } from '@env/environment';
import { AppConstants } from '@app/core/constants/app.constants';

@Injectable({
  providedIn: 'root'
})
export class EpisodeService {
  private http = inject(HttpClient);
  private storage = inject(StorageService);
  error = signal<string | null>(null);
  loading = signal(false);
  response = signal<EpisodeResponseDTO | null>(null);

  readonly episodes = computed(() => this.response()?.episodes ?? []);
  readonly totalPages = computed(() => this.response()?.totalPages ?? 0);

  getEpisodes(page: number): void {
    this.error.set(null);
    this.loading.set(true);

    const storageKey = `${AppConstants.STORAGE_KEYS.PAGE_PREFIX}${page}`;
    const storageValue = this.storage.getItem<EpisodeResponseDTO>(storageKey);
    if (storageValue) {
      this.response.set(storageValue);
      this.loading.set(false);
      return;
    }

    this.http.get<EpisodeResponseDTO>(`${environment.apiUrl}/episodes?page=${page}`).pipe(
      timeout(environment.apiTimeout),
      catchError((err: HttpErrorResponse) => {
        console.error(err);
        this.loading.set(false);
        this.error.set(AppConstants.ERROR_MESSAGES.EPISODES_FETCH_ERROR);
        return of(null);
      })
    ).subscribe((res) => {
      this.response.set(res);
      this.loading.set(false);
      this.storage.setItem(storageKey, res);
    });
  }
}
