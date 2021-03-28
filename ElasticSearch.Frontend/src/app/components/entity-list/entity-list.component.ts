import {
  ChangeDetectionStrategy,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { switchMap, takeUntil } from 'rxjs/operators';
import { ApiClient } from 'src/app/core/api-client/api-client';
import { ModalService } from 'src/app/core/modal/modal.service';
import { EntityDetailsComponent } from '../entity-details/entity-details.component';
import { Entity } from '../entity.model';
import { SearchService } from '../search-panel/search.service';

@Component({
  selector: 'app-entity-list',
  templateUrl: './entity-list.component.html',
})
export class EntityListComponent implements OnInit, OnDestroy {
  public entities$: Observable<Entity[]>;

  private readonly subscriptionDestroyer$ = new Subject<boolean>();

  constructor(
    private readonly searchService: SearchService,
    private readonly modalService: ModalService,
    private readonly apiClient: ApiClient
  ) {}

  public ngOnInit(): void {
    this.loadEntities();
  }

  public loadEntities(): void {
    this.entities$ = this.searchService.getSearchPhrase().pipe(
      switchMap((searchPhrase: string) =>
        searchPhrase
          ? this.apiClient.getData<Entity[]>('/entities/search', {
              searchPhrase,
            })
          : this.apiClient.getData<Entity[]>('/entities')
      ),
      takeUntil(this.subscriptionDestroyer$)
    );
  }

  public addEntity(): void {
    this.modalService.showModal(EntityDetailsComponent).then(
      (isSuccessed: boolean) => {
        if (isSuccessed) {
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
