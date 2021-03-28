import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ApiClient } from 'src/app/core/api-client/api-client';
import { Entity } from '../entity.model';

@Component({
  selector: 'app-entity-details',
  templateUrl: './entity-details.component.html',
})
export class EntityDetailsComponent implements OnInit, OnDestroy {
  @Input() entityId: number;
  @Input() editMode: boolean;

  public entityFormGroup: FormGroup;

  private readonly subscriptionDestroyer$ = new Subject<boolean>();

  constructor(
    public activeModal: NgbActiveModal,
    private readonly apiClient: ApiClient
  ) {}

  public ngOnInit(): void {
    this.initFormGroup();

    if (this.editMode && this.entityId) {
      this.apiClient
        .getData<Entity>(`/entities/${this.entityId}`)
        .pipe(takeUntil(this.subscriptionDestroyer$))
        .subscribe((entity: Entity) => this.setValuesToForm(entity));
    }
  }

  public save(): void {
    const data = {
      name: this.entityFormGroup.controls.name.value,
      description: this.entityFormGroup.controls.description.value,
    };

    const saveObservable = this.entityId
      ? this.apiClient.putData<any>(`/entities/${this.entityId}`, data)
      : this.apiClient.postData<void, any>('/entities', data);

    saveObservable
      .pipe(takeUntil(this.subscriptionDestroyer$))
      .subscribe(() => this.activeModal.close(true));
  }

  public cancel(): void {
    this.activeModal.close(false);
  }

  public ngOnDestroy(): void {
    this.subscriptionDestroyer$.next(true);
  }

  private initFormGroup(): void {
    this.entityFormGroup = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.maxLength(128),
      ]),
      description: new FormControl('', [
        Validators.required,
        Validators.maxLength(512),
      ]),
    });
  }

  private setValuesToForm(entity: Entity): void {
    this.entityFormGroup.controls.name.setValue(entity.name);
    this.entityFormGroup.controls.description.setValue(entity.description);
  }
}
