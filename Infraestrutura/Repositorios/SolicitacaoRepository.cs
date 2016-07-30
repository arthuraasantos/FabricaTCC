using Dominio.Model;
using Dominio.Repository;
using Seedwork.Const;
using Seedwork.Repository;
using System;
using System.Linq;


namespace Infraestrutura.Repositorios
{
    public class SolicitacaoRepository : RepositoryBase<Solicitacao>, ISolicitacaoRepository
    {
        private readonly PontoContext Context;

        public SolicitacaoRepository(PontoContext context) : base(context)
        {
            Context = context;
        }

        public int GetCountPendingHours(Guid organizationId) =>
            Context.Set<Solicitacao>().Where(p => p.Resposta == RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == organizationId).Count();

        public int GetCountResponsePendingHours(Guid organizationId) =>
            Context.Set<Solicitacao>().Where(p => p.Resposta != RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == organizationId).Count();
    }
}
