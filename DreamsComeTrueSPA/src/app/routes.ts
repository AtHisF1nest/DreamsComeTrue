import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TodoListComponent } from './todo-list/todo-list.component';
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
        { path: 'zarzadzanie', component: ManagementComponent },
        { path: 'kalendarz', component: CalendarComponent }
      ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
