import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';

import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { ManagementComponent } from './management/management.component';
import { CalendarComponent } from './calendar/calendar.component';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { TodosService } from './_services/todos.service';
import { UserService } from './_services/user.service';
import { CategoriesComponent } from './categories/categories.component';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      TodoListComponent,
      ManagementComponent,
      CalendarComponent,
      CategoriesComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      TodosService,
      UserService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
