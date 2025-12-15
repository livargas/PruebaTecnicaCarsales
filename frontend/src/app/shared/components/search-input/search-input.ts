import { Component, effect, EventEmitter, Output, signal } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-search-input',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './search-input.html',
  styleUrls: ['./search-input.css']
})
export class SearchInputComponent {
  @Output() search = new EventEmitter<string>();

  searchQuery = signal<string>('');

  constructor() {
    effect(() => {
      this.search.emit(this.searchQuery());
    });
  }
}
