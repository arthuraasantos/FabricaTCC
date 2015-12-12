using Dominio.Model;
using Dominio.Repository;
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
        private readonly MyContext Context;
        public FolgaRepository(MyContext context) :base(context)
        {
            Context = context;
        }
    }
}
