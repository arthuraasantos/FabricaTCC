using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seedwork.Repository;
using Dominio.Repository;
using Dominio.Model;

namespace Infraestrutura.Repositorios
{
    public class FeriasRepository : RepositoryBase<Ferias>, IFeriasRepository
    {
        public FeriasRepository(MyContext context): base(context)
        {

        }
    }
}
