import {
    AfterViewInit,
    ChangeDetectionStrategy,
    Component,
    ElementRef,
    EventEmitter,
    Inject,
    OnInit,
    Output,
    ViewChildren,
} from '@angular/core';
import { Order } from 'app/orders/models/order.interface';
import { MD_DIALOG_DATA, MdDialogRef } from '@angular/material';
import { OrderService } from 'app/orders/services/order.service';
import { FormGroup, FormBuilder, Validators, FormControlName } from '@angular/forms';
import { GenericValidator } from 'app/shared/generic-validator';
import { Observable } from 'rxjs/Rx';


@Component({
    selector: 'order-edit',
    templateUrl: 'order-edit.component.html',
})

export class OrderEditComponent implements OnInit, AfterViewInit {

    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

    pageTitle: string = 'Order Edit';
    order: Order;
    errorMessage: string;
    orderForm: FormGroup;

    // Use with the generic validation message class
    displayMessage: { [key: string]: string } = {};
    private validationMessages: { [key: string]: { [key: string]: string } };
    private genericValidator: GenericValidator;

    constructor( @Inject(MD_DIALOG_DATA) public data: any,
        public orderService: OrderService,
        public dialogRef: MdDialogRef<OrderEditComponent>,
        private fb: FormBuilder) {
        this.validationMessages = {
            customerId: {
                required: 'Customer ID is required.',
                numericonly: 'Customer can only be a number',
                maxlength: 'Customer ID cannot exceed 10 characters'
            },
            customerName: {
                maxlength: 'Customer Name cannot exceed 50 characters'
            },
            datePlaced: {
                required: 'Date Placed is required.'
            }
        };

        this.genericValidator = new GenericValidator(this.validationMessages);
    }

    ngOnInit() {
        this.orderForm = this.fb.group({
            customerId: ['', [Validators.required, Validators.maxLength(10), Validators.pattern('^[0-9]*$')]],
            customerName: ['', [Validators.maxLength(50)]],
            datePlaced: [new Date(), [Validators.required]]
        });

        const id = +this.data;
        console.log(id);
        this.getOrder(id);
    }

    ngAfterViewInit(): void {
        // Watch for the blur event from any input element on the form.
        const controlBlurs: Observable<any>[] = this.formInputElements
            .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));

        // Merge the blur event observable with the valueChanges observable
        Observable.merge(this.orderForm.valueChanges, ...controlBlurs).debounceTime(800).subscribe(value => {
            this.displayMessage = this.genericValidator.processMessages(this.orderForm);
        });
    }

    getOrder(id: number): void {
        this.orderService.getOrder(id).subscribe(order => this.onOrderRetrieved(order));
    }

    onOrderRetrieved(order: Order) {
        if (this.orderForm) this.orderForm.reset();

        this.order = order;

        if (this.order.id === 0 ) {
            this.pageTitle = 'Add Order';
        } else {
            this.pageTitle = `Edit Order: ${this.order.id}`;
        }

        this.orderForm.patchValue({
            customerId: this.order.customerId,
            customerName: this.order.customerName,
            datePlaced: this.order.datePlaced
        });
    }

    deleteOrder(id: number): void {
        this.orderService.deleteOrder(id)
            .subscribe(() => this.dialogRef.close(), 
                err => alert(`There was an error deleting order ${id}, please try again.`));

        //this.dialogRef.close();
    }

    saveOrder(): void {
        if (this.orderForm.dirty && this.orderForm.valid) {
            const c = Object.assign({}, this.order, this.orderForm.value);

            this.orderService.saveOrder(c)
                .subscribe(
                () => this.onSaveComplete(),
                (err) => this.errorMessage = <any>err
                );
        } else if (!this.orderForm.dirty) {
            this.onSaveComplete();
        }
    }

    onSaveComplete(): void {
        this.orderForm.reset();
        this.dialogRef.close();
    }
}
