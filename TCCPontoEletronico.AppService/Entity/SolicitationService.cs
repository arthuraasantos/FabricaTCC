using Dominio.Repository;
using System;
using TCCPontoEletronico.AppService.Interface;

namespace TCCPontoEletronico.AppService.Entity
{
    public class SolicitationService : ISolicitationService
    {
        private readonly ISolicitacaoRepository SolicitationRepository;
        private readonly IFuncionarioService EmployeeService;

        public SolicitationService(ISolicitacaoRepository solicitationRepository, IFuncionarioService employeeService)
        {
            SolicitationRepository = solicitationRepository;
            EmployeeService = employeeService;
        }

        public int GetCountPendingHours(Guid organizationId) =>
            SolicitationRepository.GetCountPendingHours(organizationId);

        public int GetCountResponsePendingHours() =>
            SolicitationRepository.GetCountResponsePendingHours(EmployeeService.GetOrganizationIdLogged());
    }
}
