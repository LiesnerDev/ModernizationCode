import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EmployeeService } from '../../core/services/employee.service';
import { EmployeeRequest } from '../../core/models/employee-request.model';

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.scss']
})
export class CreateEmployeeComponent {
  employee: EmployeeRequest = {
    id: null,
    name: '',
    age: null,
    address: ''
  };

  constructor(private employeeService: EmployeeService) {}

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.employeeService.addEmployee(this.employee).subscribe(
        () => {
          alert('Employee record has been added successfully.');
          form.resetForm();
        },
        error => {
          alert('An error occurred while adding the employee.');
          console.error(error);
        }
      );
    }
  }
}