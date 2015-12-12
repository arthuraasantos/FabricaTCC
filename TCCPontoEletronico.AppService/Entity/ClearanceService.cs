
using System;
using Dominio.Model;
using TCCPontoEletronico.AppService.Interface;
using Dominio.Repository;
using System.Linq;
using Seedwork.Const;

namespace TCCPontoEletronico.AppService.Entity
{
    public class ClearanceService : IClearanceService
    {
        private IFolgaRepository ClearanceRepository { get; }
        private IEmployeeService EmployeService { get; }

        public ClearanceService(IFolgaRepository clearanceRepository, IEmployeeService employeService)
        {
            ClearanceRepository = clearanceRepository;
            EmployeService = employeService;
        }
        public Folga GetClearanceNotificationWarning(DateTime now, Funcionario employee) =>
            ClearanceRepository
                        .Listar()
                        .Where(p => p.Data == now && p.Funcionario.Id == employee.Id && p.Resposta == RespostaSolicitacao.Aprovado)
                        .ToList()
                        .FirstOrDefault();

        public int GetCountPendingClearance() => ClearanceRepository.GetCountPendingClearance(EmployeService.GetOrganizationIdLogged());

        public int GetCountResponsePendingClearance() => ClearanceRepository.GetCountResponsePendingClearance(EmployeService.GetOrganizationIdLogged());

    }
}
