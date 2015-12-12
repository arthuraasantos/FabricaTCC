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
        private readonly MyContext Context;

        public SolicitacaoRepository(MyContext context) : base(context)
        {
            Context = context;
        }

        public int GetCountPendingHours(Guid organizationLogged)
        {
            return Context.Set<Solicitacao>().Where(p => p.Resposta == RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == organizationLogged).Count();
        }
    }
}
