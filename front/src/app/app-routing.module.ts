import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';

import {HomepageComponent} from './components/homepage/homepage.component';
import {UtilitiesComponent} from './components/utilities/utilities.component';
import {ClientsComponent} from './components/clients/clients.component';
import {ProfilepageComponent} from './components/profilepage/profilepage.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'utility',
    component: UtilitiesComponent,
  },
  {
    path: 'home',
    component: HomepageComponent,
  },
  {
    path: 'clients',
    component: ClientsComponent,
  },
  {
    path: 'profile',
    component: ProfilepageComponent,
  },
  {
    path: '**',
    component: HomepageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
