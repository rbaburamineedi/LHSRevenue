import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadonlyListComponent } from './readonly-list.component';

describe('ReadonlyListComponent', () => {
  let component: ReadonlyListComponent;
  let fixture: ComponentFixture<ReadonlyListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReadonlyListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReadonlyListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
