import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeRequest } from '../models/employee-request.model';
import { EmployeeModelResponse } from '../models/employee-response.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiUrl = 'http://localhost:5000/api/Employee';

  constructor(private http: HttpClient) {}

  addEmployee(employee: EmployeeRequest): Observable<EmployeeModelResponse> {
    return this.http.post<EmployeeModelResponse>(`${this.apiUrl}`, employee);
  }
}