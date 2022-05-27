import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialModule } from './modules/material.module';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from '@angular/cdk/layout';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { NotFoundComponent } from './pages/not-found/not-found.component';
import { HomeComponent } from './pages/home/home.component';
import { NavComponent } from './layouts/nav/nav.component';
import { AppealListComponent } from './pages/appeal-list/appeal-list.component';
import { ServerErrorComponent } from './pages/server-error/server-error.component';
import { TestErrorsComponent } from './pages/test-errors/test-errors.component';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HighlightSearchPipe } from './pipes/highlight-search.pipe';
import { EformUserListComponent } from './pages/eform-user-list/eform-user-list.component';
import { NavMenuComponent } from './layouts/nav-menu/nav-menu.component';
import { EformUserDetailComponent } from './pages/eform-user-detail/eform-user-detail.component';
import { AccountComponent } from './pages/account/account.component';
import { EformDocListComponent } from './pages/eform-doc-list/eform-doc-list.component';
import { EformUspsAddressComponent } from './pages/eform-usps-address/eform-usps-address.component';
import { SandboxComponent } from './pages/sandbox/sandbox.component';

@NgModule({
  declarations: [
    AppComponent,
    HighlightSearchPipe,
    NavComponent,
    NotFoundComponent,
    NavMenuComponent,
    AccountComponent,
    ServerErrorComponent,
    TestErrorsComponent,
    HomeComponent,
    AppealListComponent,
    EformUserListComponent,
    EformUserDetailComponent,
    EformDocListComponent,
    EformUspsAddressComponent,
    SandboxComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    MaterialModule,
    LayoutModule,
  ],
  providers: [
    HighlightSearchPipe,
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
