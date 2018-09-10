import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TodoListComponent } from './Todos/todo-list/todo-list.component';
import { TodoListItemComponent } from './Todos/todo-list-item/todo-list-item.component';
import { ManagementComponent } from './management/management.component';
import { CalendarComponent } from './calendar/calendar.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
      path: '',
      runGuardsAndResolvers: 'always',
      canActivate: [AuthGuard],
      children: [
        { path: 'nasze-cele', component: TodoListComponent },
        { path: 'nasze-cele/:id', component: TodoListItemComponent },
        { path: 'zarzadzanie', component: ManagementComponent },
        { path: 'kalendarz', component: CalendarComponent }
      ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
