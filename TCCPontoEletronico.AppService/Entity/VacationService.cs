
using System;
using Dominio.Model;
using Seedwork.Const;
using Dominio.Repository;
using Infraestrutura;
using System.Linq;
using TCCPontoEletronico.AppService.Interface;

namespace TCCPontoEletronico.AppService.Entity
{
    public class VacationService : IVacationService
    {
        private MyContext Context { get; set; }
        private readonly IFuncionarioRepository FuncionarioRepository;
        private readonly IFeriasRepository VacationRepository;
        private readonly IFuncionarioService EmployeeService;

        public VacationService(MyContext context, IFuncionarioRepository employeeRepository, IFeriasRepository vacation, IFuncionarioService employeeService)
        {
            Context = context;
            FuncionarioRepository = employeeRepository;
            VacationRepository = vacation;
            EmployeeService = employeeService;

        }
        public Ferias GetVacationNotificationWarning(DateTime now, Funcionario employee)
        {
            return VacationRepository
                    .Listar()
                    .Where(p => p.Inicio <= now.Date && p.Fim >= now.Date && p.Funcionario.Id == employee.Id && p.Resposta == RespostaSolicitacao.Aprovado)
                    .ToList()
                    .FirstOrDefault();
        }

        public int GetCountVacationPending() => VacationRepository.GetCountPendingVacation(EmployeeService.GetOrganizationIdLogged());

        public int GetCountResponsePendingVacation() => VacationRepository.GetCountResponsePendingVacation(EmployeeService.GetOrganizationIdLogged());
    }
}
