import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule, BsDatepickerModule, TimepickerModule } from 'ngx-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';

import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { TodoListComponent } from './Todos/todo-list/todo-list.component';
import { ManagementComponent } from './Managaments/management/management.component';
import { CalendarComponent } from './calendar/calendar.component';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { TodosService } from './_services/todos.service';
import { UserService } from './_services/user.service';
import { CategoriesComponent } from './categories/categories.component';
import { TodoListItemComponent } from './Todos/todo-list-item/todo-list-item.component';
import { TodoListDetailComponent } from './Todos/todo-list-detail/todo-list-detail.component';
import { TodoListDetailResolver } from './_resolvers/todo-list-detail.resolver';
import { InformationComponent } from './information/information.component';
import { TodoListResolver } from './_resolvers/todo-list.resolver';
import { ManagementService } from './_services/management.service';
import { ManagementResolver } from './_resolvers/management.resolver';
import { ManagementCategoriesResolver } from './_resolvers/management-categories.resolver';
import { ManagementTodosResolver } from './_resolvers/management-todos.resolver';
import { ManagementCategoriesComponent } from './Managaments/management-categories/management-categories.component';
import { ManagementTodosComponent } from './Managaments/management-todos/management-todos.component';
import { BreadcrumbsComponent } from './breadcrumbs/breadcrumbs.component';
import { ManagementDreamsComponent } from './Managaments/management-dreams/management-dreams.component';
import { ManagementTodosListComponent } from './Managaments/management-todos-list/management-todos-list.component';
import { ManagementDreamsResolver } from './_resolvers/management-dreams.resolver';
import { ManagementDreamsCategoriesResolver } from './_resolvers/management-dreams-categories.resolver';
import { ManagementDreamsCategoriesComponent } from './Managaments/management-dreams-categories/management-dreams-categories.component';
import { DreamListComponent } from './Todos/dream-list/dream-list.component';
import { DreamListResolver } from './_resolvers/dream-list.resolver';
import { ManagementDreamsConnectionsComponent } from './Managaments/management-dreams-connections/management-dreams-connections.component';
import { ManagementTodosConnectionsComponent } from './Managaments/management-todos-connections/management-todos-connections.component';
import { HistoryOfTodoResolver } from './_resolvers/history-of-todo.resolver';
import { RandomizeTodoComponent } from './randomize-todo/randomize-todo.component';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { FindPairComponent } from './find-pair/find-pair.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';

export function tokenGetter() {
    return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      TodoListComponent,
      ManagementComponent,
      CalendarComponent,
      CategoriesComponent,
      TodoListItemComponent,
      TodoListDetailComponent,
      InformationComponent,
      ManagementCategoriesComponent,
      ManagementTodosComponent,
      BreadcrumbsComponent,
      ManagementDreamsComponent,
      ManagementTodosListComponent,
      ManagementDreamsCategoriesComponent,
      DreamListComponent,
      ManagementDreamsConnectionsComponent,
      ManagementTodosConnectionsComponent,
      RandomizeTodoComponent,
      FindPairComponent,
      EditProfileComponent
   ],
   imports: [
      JwtModule.forRoot({
        config: {
          tokenGetter: tokenGetter,
          whitelistedDomains: ['localhost:5000'],
          blacklistedRoutes: ['localhost:5000/api/auth']
      }}),
      HttpClientModule,
      FormsModule,
      BrowserModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      TimepickerModule.forRoot(),
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      TodosService,
      UserService,
      TodoListDetailResolver,
      TodoListResolver,
      ManagementService,
      ManagementResolver,
      ManagementCategoriesResolver,
      ManagementTodosResolver,
      ManagementDreamsResolver,
      ManagementDreamsCategoriesResolver,
      DreamListResolver,
      HistoryOfTodoResolver,
      UserListResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
