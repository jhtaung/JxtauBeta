import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EformUserListComponent } from './eform-user-list.component';

describe('EformUserListComponent', () => {
  let component: EformUserListComponent;
  let fixture: ComponentFixture<EformUserListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EformUserListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EformUserListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
