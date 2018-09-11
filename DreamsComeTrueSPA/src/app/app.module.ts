import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap';
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
import { ManagementComponent } from './management/management.component';
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
      InformationComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
          config: {
              tokenGetter: tokenGetter,
              whitelistedDomains: ['localhost:5000'],
              blacklistedRoutes: ['localhost:5000/api/auth']
          }
      })
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      TodosService,
      UserService,
      TodoListDetailResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
