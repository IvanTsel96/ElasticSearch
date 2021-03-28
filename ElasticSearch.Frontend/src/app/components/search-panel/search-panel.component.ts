import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-search-panel',
  templateUrl: './search-panel.component.html',
})
export class SearchPanelComponent {
  @Output() search = new EventEmitter<string>();

  public searchPhrase: string;

  public onSearch(): void {
    this.search.emit(this.searchPhrase);
  }
}
