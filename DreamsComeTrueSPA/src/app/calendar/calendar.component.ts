import {
  Component,
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef,
  OnInit
} from '@angular/core';
import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours
} from 'date-fns';
import { Subject } from 'rxjs';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent,
  CalendarView
} from 'angular-calendar';
import { Todo } from '../_models/todo';
import { TodosService } from '../_services/todos.service';
import { EventItem } from '../_models/eventItem';

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'
  }
};

@Component({
  selector: 'app-calendar',
  styleUrls: ['./calendar.component.css'],
  templateUrl: './calendar.component.html'
})
export class CalendarComponent implements OnInit {

  todoList: Todo[] = [];
  eventList: EventItem[] = [];
  activeTodo: Todo;

  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  actions: CalendarEventAction[] = [
    {
      label: '<i class="fa fa-times"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.events = this.events.filter(iEvent => iEvent !== event);
        this.handleEvent('Deleted', event);
      }
    }
  ];

  refresh: Subject<any> = new Subject();

  events: CalendarEvent[] = [];

  activeDayIsOpen = true;

  constructor(private todosService: TodosService) {}

  ngOnInit() {
    this.todosService.getItems().subscribe(res => {
      this.todoList = res;
    });

    this.todosService.getEvents().subscribe(res => {
      this.eventList = res;
      this.eventList.forEach(event => {
        this.addEvent(event.id, event.todoItem.objective, event.plannedFor);
      });
    });
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd
  }: CalendarEventTimesChangedEvent): void {
    this.events = this.events.map(iEvent => {
      if (iEvent === event) {
        return {
          ...event,
          start: newStart,
          end: newEnd
        };
      }
      return iEvent;
    });
    this.handleEvent('Dropped or resized', event);
  }

  handleEvent(action: string, event: CalendarEvent): void {
    console.log(action, event);
    if (action === 'Deleted') {
      this.deleteEvent(event);
    } else if (action === 'Dropped or resized') {
      this.updateEvent(event);
    }
  }

  updateEvent(event: CalendarEvent): void {
    const currEvent = this.events.find(x => x.id === event.id);
    const eventToModify: EventItem = {
      id: +event.id,
      plannedFor: currEvent.end,
      todoItemId: 0,
      todoItem: null
    };

    this.todosService.modifyEvent(eventToModify).subscribe(res => {

    }, error => {
      console.log(error);
    });
  }

  todoItemClick(todoItem: Todo) {
    this.todoList.forEach(todo => todo.active = false);
    todoItem.active = true;
    this.activeTodo = todoItem;
  }

  addTodoEvent() {
    const calEvent = this.addEvent(this.activeTodo.id, this.activeTodo.objective, this.viewDate);
    const eventToAdd: EventItem = {
      id: 0,
      plannedFor: this.viewDate,
      todoItem: this.activeTodo,
      todoItemId: this.activeTodo.id
    };
    this.todosService.addEvent(eventToAdd).subscribe(res => {
      calEvent.id = res.id;
      console.log(calEvent);
      console.log(this.events);
    }, error => {
      console.log(error);
    });
  }

  addEvent(id, title, date): CalendarEvent {
    const calEvent = this.generateEvent(id, title, date);
    this.events = [
      ...this.events,
      calEvent
    ];

    return calEvent;
  }

  generateEvent(id: number, title: string, date: Date): CalendarEvent {
    return {
      id: id,
      title: id + '. ' + title,
      start: startOfDay(date),
      end: endOfDay(date),
      color: colors.blue,
      actions: this.actions,
      allDay: true,
      resizable: {
        beforeStart: true,
        afterEnd: true
      },
      draggable: true
    };
  }

  deleteEvent(eventToDelete: CalendarEvent) {
    this.todosService.deleteEvent(+eventToDelete.id).subscribe(x => {

    }, error => {
      console.log(error);
    });
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
