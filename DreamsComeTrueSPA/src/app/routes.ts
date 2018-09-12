import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TodoListComponent } from './Todos/todo-list/todo-list.component';
import { TodoListItemComponent } from './Todos/todo-list-item/todo-list-item.component';
import { ManagementComponent } from './Managaments/management/management.component';
import { CalendarComponent } from './calendar/calendar.component';
import { AuthGuard } from './_guards/auth.guard';
import { TodoListDetailComponent } from './Todos/todo-list-detail/todo-list-detail.component';
import { TodoListDetailResolver } from './_resolvers/todo-list-detail.resolver';
import { InformationComponent } from './information/information.component';
import { TodoListResolver } from './_resolvers/todo-list.resolver';
import { ManagementResolver } from './_resolvers/management.resolver';
import { ManagementCategoriesResolver } from './_resolvers/management-categories.resolver';
import { ManagementTodosResolver } from './_resolvers/management-todos.resolver';
import { ManagementTodosComponent } from './Managaments/management-todos/management-todos.component';
import { ManagementCategoriesComponent } from './Managaments/management-categories/management-categories.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
      path: '',
      runGuardsAndResolvers: 'always',
      canActivate: [AuthGuard],
      children: [
        { path: 'nasze-cele', component: TodoListComponent,
            resolve: {todoList: TodoListResolver} },
        { path: 'nasze-cele/:id', component: TodoListDetailComponent,
            resolve: {todoItem: TodoListDetailResolver} },
        { path: 'zarzadzanie', component: ManagementComponent,
            resolve: {managementTypes: ManagementResolver} },
        { path: 'zarzadzanie-celami', component: ManagementTodosComponent,
            resolve: {todoList: ManagementTodosResolver} },
        { path: 'zarzadzanie-kategoriami', component: ManagementCategoriesComponent,
            resolve: {categoryList: ManagementCategoriesResolver} },
        { path: 'kalendarz', component: CalendarComponent }
      ]
    },
    { path: 'dowiedz-sie-wiecej', component: InformationComponent },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
