import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ModalService } from 'src/app/core/modal/modal.service';
import { EntityDetailsComponent } from '../entity-details/entity-details.component';
import { Entity } from '../entity.model';

@Component({
  selector: 'app-entity-list-item',
  templateUrl: './entity-list-item.component.html',
})
export class EntityListItemComponent {
  @Input() entity: Entity;
  @Output() editEntity = new EventEmitter<number>();
  @Output() removeEntity = new EventEmitter<number>();

  constructor(private readonly modalService: ModalService) {}

  public onEdit(): void {
    this.modalService
      .showModal(EntityDetailsComponent, {
        entityId: this.entity.id,
        editMode: true,
      })
      .then(
        (isSuccessed: boolean) => {
          if (isSuccessed) {
            this.editEntity.emit(this.entity.id);
          }
        },
        // This need because if not set and click dismiss we get an exception
        () => {}
      );
  }

  public onRemove(): void {
    this.removeEntity.emit(this.entity.id);
  }
}
