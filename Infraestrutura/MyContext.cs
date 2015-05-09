using Infraestrutura.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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
            modelBuilder.Configurations.Add(new SolicitacaoDbMapping());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}
