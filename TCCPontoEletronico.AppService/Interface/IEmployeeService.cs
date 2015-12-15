﻿
using Dominio.Model;
using Seedwork.Const;
using System;
using TCCPontoEletronico.AppService.Interface.DTOs;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface IEmployeeService
    {
        string GetEmailEmployeeLogged();
        string GetNameEmployeeLogged();
        string GetOrganizationNameLogged();
        Guid GetOrganizationIdLogged();
        PerfilAcesso GetAccessProfile();
        string GetAccessProfileDescription();
        Funcionario GetEmployeeLogged();
        int GetCountEmployee();
        EmployeeDTO CreateEmployee(string employeeName, string employeeCpf, string employeeEmail, Guid organizationId, Guid officeHoursId);
        
    }
}
