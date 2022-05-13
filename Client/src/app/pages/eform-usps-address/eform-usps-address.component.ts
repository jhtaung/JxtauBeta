import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Address } from 'src/app/models/address';
import { EformService } from 'src/app/services/eform.service';

@Component({
  selector: 'app-eform-usps-address',
  templateUrl: './eform-usps-address.component.html',
  styleUrls: ['./eform-usps-address.component.css'],
})
export class EformUspsAddressComponent implements OnInit {
  address!: Address;
  form!: FormGroup;

  constructor(
    private eformService: EformService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.form = this.formBuilder.group({
      address1: 'Suite 700',
      address2: '6406 IVY LN',
      city: 'GREENBELT',
      state: 'MD',
      zip5: '20770',
      zip4: '1435',
    });
  }

  getAddress() {
    this.eformService.getAddress(this.form.value).subscribe({
      next: address => {
        console.log(address);
        this.address = address;
      },
      error: error => {
        console.log(error);
      },
      complete: () => {},
    });
  }
}
