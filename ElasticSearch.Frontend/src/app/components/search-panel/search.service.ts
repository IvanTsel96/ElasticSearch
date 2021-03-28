import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class SearchService {
  private readonly searchPhrase$ = new BehaviorSubject<string>(null);

  public getSearchPhrase(): Observable<string> {
    return this.searchPhrase$.asObservable();
  }

  public setSearchPhrase(searchPhrase: string): void {
    this.searchPhrase$.next(searchPhrase);
  }
}
