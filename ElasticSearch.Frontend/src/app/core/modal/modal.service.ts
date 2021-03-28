import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { Injectable } from '@angular/core';

@Injectable()
export class ModalService {
  constructor(
    private readonly modalService: NgbModal,
    private readonly modalConfig: NgbModalConfig
  ) {}

  public showModal(modalComponent: any, params: any = null): Promise<any> {
    this.modalConfig.centered = true;

    const modal = this.modalService.open(modalComponent, {
      backdrop: 'static',
    });

    if (params) {
      // tslint:disable-next-line: forin
      for (const propertyName in params) {
        modal.componentInstance[propertyName] = params[propertyName];
      }
    }

    return modal.result;
  }
}
