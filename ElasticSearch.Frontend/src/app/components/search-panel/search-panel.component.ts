import { Component } from '@angular/core';
import { SearchService } from './search.service';

@Component({
  selector: 'app-search-panel',
  templateUrl: './search-panel.component.html',
})
export class SearchPanelComponent {
  public searchPhrase: string;

  constructor(private readonly searchService: SearchService) {}

  public onSearch(): void {
    this.searchService.setSearchPhrase(this.searchPhrase);
  }

  public onReset(): void {
    this.searchPhrase = '';

    this.searchService.setSearchPhrase(this.searchPhrase);
  }
}
