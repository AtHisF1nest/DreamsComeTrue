/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RandomizeTodoComponent } from './randomize-todo.component';

describe('RandomizeTodoComponent', () => {
  let component: RandomizeTodoComponent;
  let fixture: ComponentFixture<RandomizeTodoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RandomizeTodoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RandomizeTodoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
