using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Dominio;
using Dominio.Model;

namespace Infraestrutura.Migrations
{
 
    internal sealed class Configuration : DbMigrationsConfiguration<Infraestrutura.MyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            
        }

        protected override void Seed(Infraestrutura.MyContext context)
        {
            var perfil = new PerfilDeAcesso()
            {
                Id = Guid.NewGuid(),
                Descricao = "Gerente"
            };
            context.Set<PerfilDeAcesso>().Add(perfil);

            var empresa = new Empresa()
            {
                Id = Guid.NewGuid(),
                RazaoSocial = "Craque do Pão e Pizzaria",
                Bairro = "Campo Grande",
                Bloqueado = "N",
                Cep = "23070420",
                Cidade = "Rio de Janeiro",
                Cnpj = "012392834000198",
                Estado = "RJ", 
                NomeFantasia = "Craque do Pão",
                NumeroEndereco = 1330,
                Pais = "Brasil",
                Logradouro = "Estrada do Campinho"
                
            };
            context.Set<Empresa>().Add(empresa);
            


            var funcionario = new Funcionario()
            {
                Id = Guid.NewGuid(),
                Nome = "Arthur",
                Email = "arthuraasantos@hotmail.com",
                Senha = "admin",
                Empresa = empresa,
                PerfilDeAcesso = perfil
            };
            context.Set<Funcionario>().Add(funcionario);
        }
    }
}
