
using Seedwork.Repository;
using Dominio.Repository;
using Dominio.Model;
using System;
using System.Linq;
using Seedwork.Const;

namespace Infraestrutura.Repositorios
{
    public class FeriasRepository : RepositoryBase<Ferias>, IFeriasRepository
    {
        private readonly MyContext Context;
        public FeriasRepository(MyContext context): base(context)
        {
            Context = context;
        }

        public int GetCountPendingVacation(Guid organizationId) =>
            Context.Set<Ferias>().Where(p => p.Resposta == RespostaSolicitacao.Nenhuma && p.Funcionario.Empresa.Id == organizationId).Count();
        
    }
}
