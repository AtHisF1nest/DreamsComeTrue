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
import { ManagementDreamsComponent } from './Managaments/management-dreams/management-dreams.component';
import { ManagementDreamsResolver } from './_resolvers/management-dreams.resolver';
import { ManagementDreamsCategoriesResolver } from './_resolvers/management-dreams-categories.resolver';
import { ManagementDreamsCategoriesComponent } from './Managaments/management-dreams-categories/management-dreams-categories.component';
import { DreamListResolver } from './_resolvers/dream-list.resolver';
import { DreamListComponent } from './Todos/dream-list/dream-list.component';
import { ManagementDreamsConnectionsComponent } from './Managaments/management-dreams-connections/management-dreams-connections.component';
import { ManagementTodosConnectionsComponent } from './Managaments/management-todos-connections/management-todos-connections.component';
import { HistoryOfTodoResolver } from './_resolvers/history-of-todo.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { FindPairComponent } from './find-pair/find-pair.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
      path: '',
      runGuardsAndResolvers: 'always',
      canActivate: [AuthGuard],
      children: [
        { path: 'nasze-cele', component: TodoListComponent,
            resolve: {todoList: TodoListResolver} },
        { path: 'nasze-marzenia', component: DreamListComponent,
            resolve: {dreamList: DreamListResolver} },
        { path: 'nasze-marzenia/:id', component: TodoListDetailComponent,
            resolve: {dreamItem: TodoListDetailResolver, historyOfDreamItem: HistoryOfTodoResolver} },
        { path: 'nasze-cele/:id', component: TodoListDetailComponent,
            resolve: {todoItem: TodoListDetailResolver, historyOfTodoItem: HistoryOfTodoResolver} },
        { path: 'zarzadzanie', component: ManagementComponent,
            resolve: {managementTypes: ManagementResolver} },
        { path: 'zarzadzanie-celami', component: ManagementTodosComponent,
            resolve: {todoList: ManagementTodosResolver} },
        { path: 'zarzadzanie-kategoriami', component: ManagementCategoriesComponent,
            resolve: {categoryList: ManagementCategoriesResolver} },
        { path: 'zarzadzanie-marzeniami', component: ManagementDreamsComponent,
            resolve: {dreamList: ManagementDreamsResolver} },
        { path: 'zarzadzanie-kategoriami-marzen', component: ManagementDreamsCategoriesComponent,
            resolve: {categoryList: ManagementDreamsCategoriesResolver} },
        { path: 'zarzadzanie-polaczeniami-celow', component: ManagementTodosConnectionsComponent,
            resolve: {categoryList: ManagementCategoriesResolver} },
        { path: 'zarzadzanie-polaczeniami-marzen', component: ManagementDreamsConnectionsComponent,
            resolve: {categoryList: ManagementDreamsCategoriesResolver} },
        { path: 'kalendarz', component: CalendarComponent },
        { path: 'znajdz-pare', component: FindPairComponent,
            resolve: {userList: UserListResolver} },
        { path: 'edycja-profilu', component: EditProfileComponent }
      ]
    },
    { path: 'dowiedz-sie-wiecej', component: InformationComponent },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
