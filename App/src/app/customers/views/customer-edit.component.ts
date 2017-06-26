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
import { Customer } from 'app/customers/models/customer.interface';
import { MD_DIALOG_DATA, MdDialogRef } from '@angular/material';
import { CustomerService } from 'app/customers/services/customer.service';
import { FormGroup, FormBuilder, Validators, FormControlName } from '@angular/forms';
import { GenericValidator } from 'app/shared/generic-validator';
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'customer-edit',
    templateUrl: 'customer-edit.component.html'
})

export class CustomerEditComponent implements OnInit, AfterViewInit {
    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
    @Output() closeDialog = new EventEmitter();
    pageTitle: string = 'Customer Edit';
    customer: Customer;
    errorMessage: string;
    customerForm: FormGroup;

    // Use with the generic validation message class
    displayMessage: { [key: string]: string } = {};
    private validationMessages: { [key: string]: { [key: string]: string } };
    private genericValidator: GenericValidator;

    constructor( @Inject(MD_DIALOG_DATA) public data: any,
        public customerService: CustomerService,
        public dialogRef: MdDialogRef<CustomerEditComponent>,
        private fb: FormBuilder) {
        this.validationMessages = {
            name: {
                required: 'Customer name is required.',
                maxlength: 'Product name cannot exceed 50 characters.'
            },
            address: {
                required: 'Customer address is required.',
                maxlength: 'Address cannot exceed 100 characters'
            },
            business: {
                required: 'Customer business is required.',
                maxlength: 'Business cannot exceed 50 characters'
            }
        };

        this.genericValidator = new GenericValidator(this.validationMessages);
    }

    ngOnInit() {
        this.customerForm = this.fb.group({
            name: ['', [Validators.required, Validators.maxLength(50)]],
            address: ['', [Validators.required, Validators.maxLength(100)]],
            business: ['', [Validators.required, Validators.maxLength(50)]]
        });

        const id = +this.data;
        this.getCustomer(id);
    }

    ngAfterViewInit(): void {
        // Watch for the blur event from any input element on the form.
        const controlBlurs: Observable<any>[] = this.formInputElements
            .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));

        // Merge the blur event observable with the valueChanges observable
        Observable.merge(this.customerForm.valueChanges, ...controlBlurs).debounceTime(800).subscribe(value => {
            this.displayMessage = this.genericValidator.processMessages(this.customerForm);
        });
    }

    getCustomer(id: number): void {
        this.customerService.getCustomer(id).subscribe(customer => this.onCustomerRetrieved(customer));
    }

    onCustomerRetrieved(customer: Customer) {
        if (this.customerForm) this.customerForm.reset();

        this.customer = customer;

        if (this.customer.id === 0 ) {
            this.pageTitle = 'Add Customer';
        } else {
            this.pageTitle = `Edit Customer: ${this.customer.name}`;
        }

        this.customerForm.patchValue({
            name: this.customer.name,
            address: this.customer.address,
            business: this.customer.business
        });
    }

    deleteCustomer(id: number): void {
        this.customerService.deleteCustomer(id)
            .subscribe(() => this.dialogRef.close(), 
                err => alert(`There was an error deleting customer ${id}, please try again.`));

        //this.dialogRef.close();
    }

    saveCustomer(): void {
        if (this.customerForm.dirty && this.customerForm.valid) {
            const c = Object.assign({}, this.customer, this.customerForm.value);

            this.customerService.saveCustomer(c)
                .subscribe(
                () => this.onSaveComplete(),
                (err) => this.errorMessage = <any>err
                );
        } else if (!this.customerForm.dirty) {
            this.onSaveComplete();
        }
    }

    onSaveComplete(): void {
        this.customerForm.reset();
        this.closeDialog.emit();
    }
}
