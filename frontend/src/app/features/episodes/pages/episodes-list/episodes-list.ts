import { Component, inject, signal, ViewChild, ElementRef, effect } from '@angular/core';
import { DatePipe, DOCUMENT, NgOptimizedImage } from '@angular/common';
import { Router } from '@angular/router';
import { EpisodeService } from '@features/episodes/services/episode.service';
import { CharacterDTO } from '@features/episodes/models/dto/character.dto';
import { Pagination } from '@shared/components/pagination/pagination';
import { SearchInputComponent } from '@shared/components/search-input/search-input';
import { SearchFilterPipe } from '@shared/pipes/search-filter.pipe';
import { CharacterModalComponent } from '@features/episodes/components/character-modal/character-modal.component';
import { AppConstants } from '@app/core/constants/app.constants';

@Component({
  selector: 'app-episodes-list',
  imports: [Pagination, SearchInputComponent, SearchFilterPipe, DatePipe, CharacterModalComponent, NgOptimizedImage],
  templateUrl: './episodes-list.html',
  styleUrls: ['./episodes-list.css', './episodes-character.css']
})
export class EpisodesListComponent {
  public readonly episodeService = inject(EpisodeService);
  public readonly router = inject(Router);
  private document: Document = inject(DOCUMENT);

  mainContent = this.document.querySelector('main');
  selectedCharacter = signal<CharacterDTO | null>(null);
  activeTransitionCharacter = signal<string | null>(null);

  expandedEpisodeId = signal<number | null>(null);
  search = signal('');
  currentPage = signal(1);

  constructor() {
    effect(() => {
      this.episodeService.getEpisodes(this.currentPage());
    });
  }

  onPageChange(page: number): void {
    this.currentPage.set(page);
    this.expandedEpisodeId.set(null);
    this.mainContent?.scrollTo({ top: 0, behavior: 'smooth' });
  }

  onEpisodeClick(id: number): void {
    if (this.expandedEpisodeId() === id) {
      this.expandedEpisodeId.set(null);
    } else {
      this.expandedEpisodeId.set(id);
      setTimeout(() => {
        const element = this.document.getElementById('episode-' + id);

        if (element && this.mainContent) {
          const headerOffset = AppConstants.UI.HEADER_OFFSET;
          const elementRect = element.getBoundingClientRect();
          const mainRect = this.mainContent.getBoundingClientRect();

          const scrollTop = this.mainContent.scrollTop;
          const targetPosition = scrollTop + (elementRect.top - mainRect.top) - headerOffset;

          this.mainContent.scrollTo({
            top: targetPosition,
            behavior: 'smooth'
          });
        }
      }, AppConstants.UI.SCROLL_DELAY);
    }
  }

  onCharacterClick(character: CharacterDTO) {
    this.activeTransitionCharacter.set(character.name);

    setTimeout(() => {
      if (!this.document.startViewTransition) {
        this.selectedCharacter.set(character);
        this.activeTransitionCharacter.set(null);
        return;
      }

      this.document.startViewTransition(async () => {
        this.selectedCharacter.set(character);
        this.activeTransitionCharacter.set(null);

        await this.waitForElement('.modal-image');
      });
    }, 0);
  }

  closeDialog(): void {
    this.selectedCharacter.set(null);
  }

  private waitForElement(selector: string): Promise<void> {
    return new Promise(resolve => {
      if (this.document.querySelector(selector)) {
        return resolve();
      }

      const observer = new MutationObserver((_, obs) => {
        if (this.document.querySelector(selector)) {
          obs.disconnect();
          resolve();
        }
      });

      observer.observe(this.document.body, {
        childList: true,
        subtree: true
      });

      setTimeout(() => {
        observer.disconnect();
        resolve();
      }, AppConstants.UI.VIEW_TRANSITION_TIMEOUT);
    });
  }
}
