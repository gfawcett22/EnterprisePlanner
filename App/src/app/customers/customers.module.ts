import { NgModule } from '@angular/core';

import { CustomerListComponent } from './customer-list.component';
import { SharedModule } from 'app/shared/shared.module';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild([
            {path: '', component: CustomerListComponent}
        ])
    ],
    exports: [],
    declarations: [CustomerListComponent],
    providers: [

    ],
})
export class CustomersModule { }
