import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Entity } from '../entity.model';

@Component({
  selector: 'app-entity-list-item',
  templateUrl: './entity-list-item.component.html',
})
export class EntityListItemComponent {
  @Input() entity: Entity;
  @Output() editEntity = new EventEmitter<number>();
  @Output() removeEntity = new EventEmitter<number>();

  public onEdit(): void {
    this.editEntity.emit(this.entity.id);
  }

  public onRemove(): void {
    this.removeEntity.emit(this.entity.id);
  }
}
