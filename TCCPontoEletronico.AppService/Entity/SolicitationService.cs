using Dominio.Repository;
using System;
using TCCPontoEletronico.AppService.Interface;

namespace TCCPontoEletronico.AppService.Entity
{
    public class SolicitationService : ISolicitationService
    {
        private readonly ISolicitacaoRepository SolicitationRepository;

        public SolicitationService(ISolicitacaoRepository solicitationRepository)
        {
            SolicitationRepository = solicitationRepository;
        }

        public int GetCountPendingHours(Guid organizationLogged)
        {
            return SolicitationRepository.GetCountPendingHours(organizationLogged);
        }
    }
}
