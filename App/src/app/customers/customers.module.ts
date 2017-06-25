import { NgModule } from '@angular/core';
import { SharedModule } from 'app/shared/shared.module';
import { RouterModule } from '@angular/router';

import { CustomerListComponent } from './views/customer-list.component';
import { CustomerItemComponent } from './views/customer-item.component';
import { CustomerDetailComponent } from 'app/customers/views/customer-detail.component';
import { CustomerEditComponent } from 'app/customers/views/customer-edit.component';
import { CustomerService } from 'app/customers/services/customer.service';

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild([
            {path: '', component: CustomerListComponent},
            {path: ':id', component: CustomerDetailComponent},
            {path: ':id/edit', component: CustomerEditComponent}
        ])
    ],
    exports: [
        CustomerListComponent
    ],
    declarations: [
        CustomerListComponent,
        CustomerItemComponent,
        CustomerDetailComponent,
        CustomerEditComponent
        ],
    providers: [
        CustomerService
    ],
})
export class CustomersModule { }
