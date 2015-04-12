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
    public class FolhaPontoRepository : RepositoryBase<FolhaPonto>, IFolhaPontoRepository
    {
         public FolhaPontoRepository(MyContext context) : base(context)
        {

        }
    }
}
