using Dominio.Model;
using Dominio.Repository;
using Seedwork.Repository;

namespace Infraestrutura.Repositorios
{
    public class PontoRepository : RepositoryBase<Ponto>, IPontoRepository
    {
        private MyContext Context { get; set; }

        public PontoRepository(MyContext context) : base(context)
        {
            Context = context;
        }

       
    }
}
