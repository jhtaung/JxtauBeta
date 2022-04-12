import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { NotFoundComponent } from './pages/not-found/not-found.component';
import { HomeComponent } from './pages/home/home.component';
import { AppealListComponent } from './pages/appeal-list/appeal-list.component';
import { ServerErrorComponent } from './pages/server-error/server-error.component';
import { TestErrorsComponent } from './pages/test-errors/test-errors.component';
import { EformUserListComponent } from './pages/eform-user-list/eform-user-list.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'appeals', component: AppealListComponent },
  { path: 'eform-users', component: EformUserListComponent },
  { path: 'test-errors', component: TestErrorsComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', redirectTo: '/404', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
