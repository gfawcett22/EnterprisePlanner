import { PagingParameters } from '../../shared/models/paging-parameters.interface';

export interface CustomerPagingParameters extends PagingParameters {
    name: string;
    address: string;
    business: string;
}