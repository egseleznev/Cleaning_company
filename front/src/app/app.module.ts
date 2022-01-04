import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { SharedModule } from './shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import {FooterComponent} from './components/footer/footer.component';
import { MenuComponent } from './components/menu/menu.component';
import {MatMenuModule } from '@angular/material/menu';
import {MatIconModule} from '@angular/material/icon';
import { LoginComponent } from './components/login/login.component';
import {RouterModule} from '@angular/router';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatCardModule} from '@angular/material/card';
import { UtilitiesComponent } from './components/utilities/utilities.component';
import { UtilityComponent } from './components/utilities/utility/utilitycard/utility.component';
import { HomepageComponent } from './components/homepage/homepage.component';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import { UtilitytableComponent } from './components/utilities/utility/utilitytable/utilitytable/utilitytable.component';
import {MatTableModule} from '@angular/material/table';
import {FormsModule} from '@angular/forms';
import { ClientsComponent } from './components/clients/clients.component';
import { CardclientComponent } from './components/clients/client/cardclient/cardclient.component';
import {MatSortModule} from '@angular/material/sort';
import { ProfilepageComponent } from './components/profilepage/profilepage.component';
import {HTTPInterceptorService} from './services/http-interceptor.service';
import {MatPaginatorModule} from '@angular/material/paginator';
import {TableclientComponent} from './components/clients/client/tableclient/tableclient.component';
import {MatButtonModule} from '@angular/material/button';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    MenuComponent,
    LoginComponent,
    UtilitiesComponent,
    UtilityComponent,
    HomepageComponent,
    UtilitytableComponent,
    ClientsComponent,
    CardclientComponent,
    TableclientComponent,
    ProfilepageComponent,
  ],
  imports: [
    MatSlideToggleModule,
    BrowserModule,
    SharedModule,
    RouterModule,
    BrowserAnimationsModule,
    MatMenuModule,
    MatIconModule,
    AppRoutingModule,
    MatFormFieldModule,
    MatCardModule,
    MatTableModule,
    FormsModule,
    MatSortModule,
    MatPaginatorModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HTTPInterceptorService,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
