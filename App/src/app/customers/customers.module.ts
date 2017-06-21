import { NgModule } from '@angular/core';

import { CustomerListComponent } from './customer-list.component';
import { SharedModule } from 'app/shared/shared.module';
import { RouterModule } from '@angular/router';
import { CustomerItemComponent } from './customer-item.component';

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild([
            {path: '', component: CustomerListComponent}
        ])
    ],
    exports: [],
    declarations: [CustomerListComponent, CustomerItemComponent],
    providers: [

    ],
})
export class CustomersModule { }
