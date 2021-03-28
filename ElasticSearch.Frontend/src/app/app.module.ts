import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ApiClient } from './core/api-client/api-client';
import { HttpClientModule } from '@angular/common/http';
import { EntityDetailsComponent } from './components/entity-details/entity-details.component';
import { EntityListComponent } from './components/entity-list/entity-list.component';
import { EntityListItemComponent } from './components/entity-list-item/entity-list-item.component';
import { SearchPanelComponent } from './components/search-panel/search-panel.component';
import { ModalService } from './core/modal/modal.service';

@NgModule({
  declarations: [
    AppComponent,
    EntityDetailsComponent,
    EntityListComponent,
    EntityListItemComponent,
    SearchPanelComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
  ],
  providers: [ModalService, ApiClient],
  bootstrap: [AppComponent],
})
export class AppModule {}
