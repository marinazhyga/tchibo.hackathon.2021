import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewFamilyMemberComponent } from './new-family-member.component';

describe('NewFamilyMemberComponent', () => {
  let component: NewFamilyMemberComponent;
  let fixture: ComponentFixture<NewFamilyMemberComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewFamilyMemberComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewFamilyMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
