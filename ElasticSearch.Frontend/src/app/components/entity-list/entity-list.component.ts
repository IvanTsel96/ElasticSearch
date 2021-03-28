import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ApiClient } from 'src/app/core/api-client/api-client';
import { Entity } from '../entity.model';

@Component({
  selector: 'app-entity-list',
  templateUrl: './entity-list.component.html',
})
export class EntityListComponent implements OnInit, OnDestroy {
  public entities$: Observable<Entity[]>;

  private readonly subscriptionDestroyer$ = new Subject<boolean>();

  constructor(private readonly apiClient: ApiClient) {}

  public ngOnInit(): void {
    this.loadEntities();
  }

  public searchEntity(searchPhrase: string): void {
    this.entities$ = this.apiClient
      .getData<Entity[]>('/entities/search', { searchPhrase })
      .pipe(takeUntil(this.subscriptionDestroyer$));
  }

  public removeEntity(id: number): void {
    this.apiClient
      .deleteData(`/entities/${id}`)
      .pipe(takeUntil(this.subscriptionDestroyer$))
      .subscribe(() => this.loadEntities());
  }

  public ngOnDestroy(): void {
    this.subscriptionDestroyer$.next(true);
  }

  private loadEntities(): void {
    this.entities$ = this.apiClient
      .getData<Entity[]>('/entities')
      .pipe(takeUntil(this.subscriptionDestroyer$));
  }
}
