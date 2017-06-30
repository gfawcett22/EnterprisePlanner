import { TableFilterComponent } from '../table-filter/table-filter.component';
import { SharedModule } from '../../shared/shared.module';
import { TitleComponent } from './cells/title/table-title.component';
import { TheadFitlersRowComponent } from './rows/thead-filters-row.component';
import { TheadTitlesRowComponent } from './rows/thead-titles-row.component';
import { ColumnTitleComponent } from './cells/column-title.component';
import { NgModule } from '@angular/core';

import { THeadComponent } from './thead.component';

@NgModule({
    imports: [SharedModule],
    exports: [THeadComponent],
    declarations: [
        THeadComponent,
        ColumnTitleComponent,
        TheadTitlesRowComponent,
        TheadFitlersRowComponent,
        TitleComponent,
        TableFilterComponent
    ],
    providers: [],
})
export class THeadModule { }
