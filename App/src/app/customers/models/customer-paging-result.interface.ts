import { Customer } from './customer.interface';
export interface CustomerPagingResult {
    totalResultCount: number;
    data: Customer[];
}