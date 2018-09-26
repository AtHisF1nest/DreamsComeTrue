import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-randomize-todo',
  templateUrl: './randomize-todo.component.html',
  styleUrls: ['./randomize-todo.component.css']
})
export class RandomizeTodoComponent implements OnInit {

  @Input() todoItems = [];

  constructor(private router: Router) { }

  ngOnInit() {
  }

  randomizeTodo() {
    const idsOfTodos = [];
    this.todoItems.forEach(x => idsOfTodos.push(x.id));

    const randomized = Math.floor(Math.random() * 100 % this.todoItems.length);

    this.router.navigate(['/nasze-cele/' + idsOfTodos[randomized]]);
  }
}
