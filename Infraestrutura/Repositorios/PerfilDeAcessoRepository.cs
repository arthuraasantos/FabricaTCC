using Dominio;
using Dominio.Model;
using Dominio.Repositorios;
using Dominio.Repository;
using Seedwork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorios
{
    public class PerfilDeAcessoRepository : RepositoryBase<PerfilDeAcesso>, IPerfilDeAcessoRepository
    {
        public PerfilDeAcessoRepository(MyContext context) : base(context)
        {

        }
    }
}
