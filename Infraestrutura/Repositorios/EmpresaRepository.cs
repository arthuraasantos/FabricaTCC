using Dominio.Model;
using Dominio.Repository;
using Seedwork.Repository;
using System.Linq;
using System;

namespace Infraestrutura.Repositorios
{
    public class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository
    {
        private readonly PontoContext Contexto;
        public EmpresaRepository(PontoContext context) : base(context)
        {
            Contexto = context;
        }

        public int GetCountOrganizations() => Contexto.Set<Empresa>().Count();

        public Guid GetOrganizationId(string fantasyName) => Contexto.Set<Empresa>().FirstOrDefault(e => e.NomeFantasia == fantasyName).Id;
        
    }
}
