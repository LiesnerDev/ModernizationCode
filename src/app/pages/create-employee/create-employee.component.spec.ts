import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CreateEmployeeComponent } from './create-employee.component';
import { EmployeeService } from '../../core/services/employee.service';
import { FormsModule } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { EmployeeRequest } from '../../core/models/employee-request.model';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CreateEmployeeComponent', () => {
  let component: CreateEmployeeComponent;
  let fixture: ComponentFixture<CreateEmployeeComponent>;
  let employeeService: EmployeeService;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [CreateEmployeeComponent],
      imports: [FormsModule, HttpClientTestingModule],
      providers: [
        {
          provide: EmployeeService,
          useValue: {
            addEmployee: jest.fn()
          }
        }
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEmployeeComponent);
    component = fixture.componentInstance;
    employeeService = TestBed.inject(EmployeeService);
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('deve salvar o funcionário quando o formulário for válido', () => {
    const employee: EmployeeRequest = { id: 1234, name: 'John Doe', age: 30, address: '123 Street' };
    component.employee = employee;
    const form = {
      valid: true,
      resetForm: jest.fn()
    };

    (employeeService.addEmployee as jest.Mock).mockReturnValue(of({ ...employee }));

    window.alert = jest.fn();

    component.onSubmit(form as any);

    expect(employeeService.addEmployee).toHaveBeenCalledWith(employee);
    expect(window.alert).toHaveBeenCalledWith('Employee record has been added successfully.');
    expect(form.resetForm).toHaveBeenCalled();
  });

  it('deve mostrar erro quando o formulário for inválido', () => {
    const form = {
      valid: false,
      resetForm: jest.fn()
    };

    window.alert = jest.fn();

    component.onSubmit(form as any);

    expect(employeeService.addEmployee).not.toHaveBeenCalled();
    // Aqui você pode adicionar expectativas adicionais para verificar mensagens de erro exibidas
  });

  it('deve mostrar alerta de erro quando o serviço falhar', () => {
    const employee: EmployeeRequest = { id: 1234, name: 'John Doe', age: 30, address: '123 Street' };
    component.employee = employee;
    const form = {
      valid: true,
      resetForm: jest.fn()
    };

    const error = new Error('Service error');
    (employeeService.addEmployee as jest.Mock).mockReturnValue(throwError(error));

    window.alert = jest.fn();
    console.error = jest.fn();

    component.onSubmit(form as any);

    expect(employeeService.addEmployee).toHaveBeenCalledWith(employee);
    expect(window.alert).toHaveBeenCalledWith('An error occurred while adding the employee.');
    expect(console.error).toHaveBeenCalledWith(error);
    expect(form.resetForm).not.toHaveBeenCalled();
  });
});