import { Component, ElementRef, ViewChild, effect, input, output, signal } from '@angular/core';
import { CharacterDTO } from '@features/episodes/models/dto/character.dto';

@Component({
  selector: 'app-character-modal',
  standalone: true,
  imports: [],
  templateUrl: './character-modal.component.html',
  styleUrl: './character-modal.component.css'
})
export class CharacterModalComponent {
  character = input<CharacterDTO | null>(null);
  close = output<void>();

  private _dialogEl = signal<HTMLDialogElement | undefined>(undefined);

  @ViewChild('dialog') set dialogRef(el: ElementRef<HTMLDialogElement>) {
    this._dialogEl.set(el?.nativeElement);
  }

  constructor() {
    effect(() => {
      const char = this.character();
      const el = this._dialogEl();

      if (char && el) {
        el.showModal();
      }
    });
  }

  onClose() {
    this._dialogEl()?.close();
    this.close.emit();
  }

  onBackdropClick(event: MouseEvent) {
    if (event.target === this._dialogEl()) {
      this.onClose();
    }
  }
}
