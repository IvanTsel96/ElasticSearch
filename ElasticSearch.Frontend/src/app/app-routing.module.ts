import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EntityListComponent } from './components/entity-list/entity-list.component';

const routes: Routes = [
  {
    path: 'entities',
    component: EntityListComponent,
  },
  {
    path: '**',
    redirectTo: 'entities',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
