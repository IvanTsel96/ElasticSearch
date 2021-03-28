import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search-panel',
  templateUrl: './search-panel.component.html',
})
export class SearchPanelComponent {
  @Output() search = new EventEmitter<string>();
  @Output() reset = new EventEmitter<any>();

  public searchPhrase: string;

  constructor(private readonly router: Router) {}

  public onSearch(): void {
    this.search.emit(this.searchPhrase);
  }

  public onReset(): void {
    this.searchPhrase = '';

    this.reset.emit();
  }
}
