using Dominio.Model;
using Dominio.Repository;
using Seedwork.Repository;

namespace Infraestrutura.Repositorios
{
    public class PontoRepository : RepositoryBase<Ponto>, IPontoRepository
    {
        private PontoContext Context { get; set; }

        public PontoRepository(PontoContext context) : base(context)
        {
            Context = context;
        }

       
    }
}
