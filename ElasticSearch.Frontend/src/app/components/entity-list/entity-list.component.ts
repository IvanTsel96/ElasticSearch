import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ApiClient } from 'src/app/core/api-client/api-client';
import { ModalService } from 'src/app/core/modal/modal.service';
import { EntityDetailsComponent } from '../entity-details/entity-details.component';
import { Entity } from '../entity.model';

@Component({
  selector: 'app-entity-list',
  templateUrl: './entity-list.component.html',
})
export class EntityListComponent implements OnInit, OnDestroy {
  public entities$: Observable<Entity[]>;

  private readonly subscriptionDestroyer$ = new Subject<boolean>();

  constructor(
    private modalService: ModalService,
    private readonly apiClient: ApiClient
  ) {}

  public ngOnInit(): void {
    this.loadEntities();
  }

  public loadEntities(): void {
    this.entities$ = this.apiClient
      .getData<Entity[]>('/entities')
      .pipe(takeUntil(this.subscriptionDestroyer$));
  }

  public searchEntity(searchPhrase: string): void {
    this.entities$ = this.apiClient
      .getData<Entity[]>('/entities/search', { searchPhrase })
      .pipe(takeUntil(this.subscriptionDestroyer$));
  }

  public addEntity(): void {
    this.modalService.showModal(EntityDetailsComponent).then(
      (isSaved: boolean) => {
        if (isSaved) {
          this.loadEntities();
        }
      },
      // This need because if not set and click dismiss we get an exception
      () => {}
    );
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
}
