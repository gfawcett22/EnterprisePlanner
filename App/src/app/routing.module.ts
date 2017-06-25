import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WelcomeComponent } from 'app/welcome/welcome.component';
import { PageNotFoundComponent } from 'app/error/page-not-found.component';


const routes: Routes = [
  {path: 'welcome', component: WelcomeComponent},
  {path: 'customers', loadChildren: 'app/customers/customers.module#CustomersModule'},
  // {path: 'orders', loadChildren: ''},
  {path: '', redirectTo: 'welcome', pathMatch: 'full'},
  {path: '**', component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class RoutingModule { }
