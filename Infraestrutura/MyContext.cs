using Infraestrutura.Mapeamento;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura
{
    public class MyContext : DbContext
    {
        public MyContext() :base(ConfigurationManager.ConnectionStrings["pontoConn"].ConnectionString)
        {
            var ensureDllIsCopied =
                System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FuncionarioDbMapping());
            modelBuilder.Configurations.Add(new PerfilDeAcessoDbMapping());
            modelBuilder.Configurations.Add(new PontoDbMapping());
            modelBuilder.Configurations.Add(new EmpresaDbMapping());
            modelBuilder.Configurations.Add(new SolicitacaoDbMapping());
            modelBuilder.Configurations.Add(new FeriasDbMapping());
            modelBuilder.Configurations.Add(new FolgaDbMapping());
            modelBuilder.Configurations.Add(new HorarioDeExpedienteDBMapping());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}
