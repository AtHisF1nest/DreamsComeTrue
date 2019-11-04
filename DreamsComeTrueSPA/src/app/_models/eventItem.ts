import { Todo } from './todo';

export interface EventItem {
    id: number;
    todoItemId: number;
    todoItem: Todo;
    plannedFor: Date;
}
