import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UtilitytableComponent } from './utilitytable.component';

describe('UtilitytableComponent', () => {
  let component: UtilitytableComponent;
  let fixture: ComponentFixture<UtilitytableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UtilitytableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UtilitytableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
