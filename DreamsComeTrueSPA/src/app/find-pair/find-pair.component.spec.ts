/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FindPairComponent } from './find-pair.component';

describe('FindPairComponent', () => {
  let component: FindPairComponent;
  let fixture: ComponentFixture<FindPairComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FindPairComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FindPairComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
