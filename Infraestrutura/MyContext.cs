using Infraestrutura.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura
{
    public class MyContext : DbContext
    {
        public MyContext() :base()
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FuncionarioDbMapping());
            modelBuilder.Configurations.Add(new PerfilDeAcessoDbMapping());
            modelBuilder.Configurations.Add(new PontoDbMapping());
            modelBuilder.Configurations.Add(new EmpresaDbMapping());

            base.OnModelCreating(modelBuilder);
        }

    }
}
