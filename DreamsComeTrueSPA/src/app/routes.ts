import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TodoListComponent } from './Todos/todo-list/todo-list.component';
import { TodoListItemComponent } from './Todos/todo-list-item/todo-list-item.component';
import { ManagementComponent } from './management/management.component';
import { CalendarComponent } from './calendar/calendar.component';
import { AuthGuard } from './_guards/auth.guard';
import { TodoListDetailComponent } from './Todos/todo-list-detail/todo-list-detail.component';
import { TodoListDetailResolver } from './_resolvers/todo-list-detail.resolver';
import { InformationComponent } from './information/information.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
      path: '',
      runGuardsAndResolvers: 'always',
      canActivate: [AuthGuard],
      children: [
        { path: 'nasze-cele', component: TodoListComponent },
        { path: 'nasze-cele/:id', component: TodoListDetailComponent,
            resolve: {todoItem: TodoListDetailResolver} },
        { path: 'zarzadzanie', component: ManagementComponent },
        { path: 'kalendarz', component: CalendarComponent }
      ]
    },
    { path: 'dowiedz-sie-wiecej', component: InformationComponent },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
