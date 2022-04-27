import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EformDocListComponent } from './eform-doc-list.component';

describe('EformDocListComponent', () => {
  let component: EformDocListComponent;
  let fixture: ComponentFixture<EformDocListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EformDocListComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EformDocListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
