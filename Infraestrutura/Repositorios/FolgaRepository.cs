using Dominio.Model;
using Dominio.Repository;
using Seedwork.Const;
using Seedwork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorios
{
    public class FolgaRepository : RepositoryBase<Folga>,IFolgaRepository
    {
        private readonly PontoContext Context;
        public FolgaRepository(PontoContext context) :base(context)
        {
            Context = context;
        }

        public int GetCountPendingClearance(Guid organizationId) => 
            Context.Set<Folga>().Where(p => p.Resposta == RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == organizationId).Count();

        public int GetCountResponsePendingClearance(Guid organizationId) =>
            Context.Set<Folga>().Where(p => p.Resposta != RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == organizationId).Count();

    }
}
