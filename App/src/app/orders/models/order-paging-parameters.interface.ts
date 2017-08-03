import { PagingParameters } from '../../shared/models/paging-parameters.interface';

export interface OrderPagingParameters extends PagingParameters {
    id: number;
    customerId: number;
    customerName: string;
    datePlaced: Date;
}