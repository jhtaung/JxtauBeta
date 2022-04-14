import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EformUserDetailComponent } from './eform-user-detail.component';

describe('EformUserDetailComponent', () => {
  let component: EformUserDetailComponent;
  let fixture: ComponentFixture<EformUserDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EformUserDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EformUserDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
