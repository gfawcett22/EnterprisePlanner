import { Order } from './order.interface';

export interface OrderPagingResult {
    totalResultCount: number;
    data: Order[];
}