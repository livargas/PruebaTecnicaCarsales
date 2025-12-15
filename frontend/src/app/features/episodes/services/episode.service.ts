import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal, computed } from '@angular/core';
import { catchError, of, timeout } from 'rxjs';
import { StorageService } from '@app/core/service/storage.service';
import { EpisodeResponseDTO } from '@features/episodes/models/dto/episodeResponse.dto';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class EpisodeService {
  private http = inject(HttpClient);
  private storage = inject(StorageService);
  private readonly apiUrl = '/api/episodes';
  error = signal<string | null>(null);
  loading = signal(false);
  response = signal<EpisodeResponseDTO | null>(null);

  readonly episodes = computed(() => this.response()?.episodes ?? []);
  readonly totalPages = computed(() => this.response()?.totalPages ?? 0);

  getEpisodes(page: number): void {
    this.error.set(null);
    this.loading.set(true);

    const storageKey = `page_${page}`;
    const storageValue = this.storage.getItem<EpisodeResponseDTO>(storageKey);
    if (storageValue) {
      this.response.set(storageValue);
      this.loading.set(false);
      return;
    }

    this.http.get<EpisodeResponseDTO>(`${this.apiUrl}?page=${page}`).pipe(
      timeout(environment.apiTimeout),
      catchError((err) => {
        console.error(err);
        this.loading.set(false);
        this.error.set('¡Wubba Lubba Dub Dub!, Error al obtener episodios');
        return of(null);
      })
    ).subscribe((res) => {
      this.response.set(res);
      this.loading.set(false);
      this.storage.setItem(storageKey, res);
    });
  }
}
