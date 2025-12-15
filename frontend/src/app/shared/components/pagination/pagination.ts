import { Component, computed, input, output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  standalone: true,
  templateUrl: './pagination.html',
  styleUrls: ['./pagination.css']
})
export class Pagination {
  currentPage = input(1);
  totalPages = input(1);
  hasNext = computed(() => this.currentPage() < this.totalPages());
  hasPrev = computed(() => this.currentPage() > 1);

  pageChange = output<number>();

  onNext() {
    if (this.hasNext()) {
      this.pageChange.emit(this.currentPage() + 1);
    }
  }

  onPrev() {
    if (this.hasPrev()) {
      this.pageChange.emit(this.currentPage() - 1);
    }
  }
}
