import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateEmployeeComponent } from './pages/create-employee/create-employee.component';

const routes: Routes = [
  { path: 'create-employee', component: CreateEmployeeComponent },
  { path: '', redirectTo: '/create-employee', pathMatch: 'full' },
  { path: '**', redirectTo: '/create-employee' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }