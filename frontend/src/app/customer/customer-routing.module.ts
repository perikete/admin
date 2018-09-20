import { Routes, RouterModule } from '@angular/router';
import { CustomerComponent } from './customer.component';
import { DetailsComponent } from './details/details.component';
import { NgModule } from '@angular/core';
import { AuthGuard } from '../core/auth/auth-guard';

const customerRoutes: Routes = [
  {
    path: 'customers',
    canActivate: [AuthGuard],
    children: [
      {
        path: ':id',
        component: DetailsComponent
      },
      {
        path: '',
        component: CustomerComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(customerRoutes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule {}
