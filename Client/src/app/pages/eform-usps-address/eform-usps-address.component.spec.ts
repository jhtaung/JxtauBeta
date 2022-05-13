import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EformUspsAddressComponent } from './eform-usps-address.component';

describe('EformUspsAddressComponent', () => {
  let component: EformUspsAddressComponent;
  let fixture: ComponentFixture<EformUspsAddressComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EformUspsAddressComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EformUspsAddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
