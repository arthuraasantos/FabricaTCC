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
                Id = Guid.Parse("09c15773-d1a5-4daa-a3b3-e8c6741d63fd"),
                Descricao = "Gerente"
            };
            context.Set<PerfilDeAcesso>().Add(perfil);

            var empresa = new Empresa()
            {
                Id = Guid.Parse("e6a1d40d-05bf-4a81-b1fd-72893c07a23e"),
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
                Id = Guid.Parse("69d2fad2-9ffe-4f2d-ad85-3ff4b277f805"),
                Nome = "Arthur",
                Email = "arthuraasantos@hotmail.com",
                Senha = "admin",
                Empresa = empresa,
                PerfilDeAcesso = perfil
            };

            var funcionarioMarlon = new Funcionario()
            {
                Id = Guid.NewGuid(),
                Nome = "Marlon",
                Email = "marlonvss@gmail.com",
                Senha = "admin",
                Empresa = empresa,
                PerfilDeAcesso = perfil
            };

            var funcionarioCharles = new Funcionario()
            {
                Id = Guid.NewGuid(),
                Nome = "Charles",
                Email = "charles.info@ymail.com",
                Senha = "admin",
                Empresa = empresa,
                PerfilDeAcesso = perfil
            };            

            context.Set<Funcionario>().Add(funcionario);
            context.Set<Funcionario>().Add(funcionarioMarlon);
            context.Set<Funcionario>().Add(funcionarioCharles);
        }
    }
}
