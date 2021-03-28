import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ApiClient } from './core/api-client/api-client';
import { HttpClientModule } from '@angular/common/http';
import { EntityDetailsComponent } from './components/entity-details/entity-details.component';
import { EntityListComponent } from './components/entity-list/entity-list.component';
import { EntityListItemComponent } from './components/entity-list-item/entity-list-item.component';
import { SearchPanelComponent } from './components/search-panel/search-panel.component';

import { ModalModule } from 'ngx-bootstrap/modal';

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
    HttpClientModule,
    ModalModule.forRoot(),
  ],
  providers: [ApiClient],
  bootstrap: [AppComponent],
})
export class AppModule {}
