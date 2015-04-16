using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Dominio;
using Dominio.Model;
using SeedWork.Tools;

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
            #region 'Perfil de Acesso'
            
            var perfil = new PerfilDeAcesso()
            {
                Id = Guid.Parse("09c15773-d1a5-4daa-a3b3-e8c6741d63fd"),
                Descricao = "Administrador"
            };

            var perfilRH = new PerfilDeAcesso()
            {
                Id = Guid.Parse("4036b285-e7fb-4ac0-aac4-5196fde05dd8"),
                Descricao = "Gerente/RH"
            };

            var perfilFuncionario = new PerfilDeAcesso()
            {
                Id = Guid.Parse("3acc5625-0663-47c2-bb4d-e507ee5b004f"),
                Descricao = "FuncionarioComum"
            };

            context.Set<PerfilDeAcesso>().Add(perfil);
            context.Set<PerfilDeAcesso>().Add(perfilRH);
            context.Set<PerfilDeAcesso>().Add(perfilFuncionario);

            #endregion

            #region 'Empresas'
            
            var empresa = new Empresa()
            {
                Id = Guid.Parse("e6a1d40d-05bf-4a81-b1fd-72893c07a23e"),
                RazaoSocial = "Empresa Administrador",
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

            var empresaRH = new Empresa()
            {
                Id = Guid.Parse("bbd66ce1-9feb-4eb5-8611-7a1a8b59f3c2"),
                RazaoSocial = "Empresa Rh",
                Bairro = "Centro",
                Bloqueado = "N",
                Cep = "23070400",
                Cidade = "Rio de Janeiro",
                Cnpj = "87335085000147",
                Estado = "RJ",
                NomeFantasia = "RH Consultoria",
                NumeroEndereco = 1330,
                Pais = "Brasil",
                Logradouro = "Estrada do Lavradio"

            };

            context.Set<Empresa>().Add(empresa);
            context.Set<Empresa>().Add(empresaRH);

            #endregion

            #region 'Funcionários'
            
            var funcionario = new Funcionario()
            {
                Id = Guid.Parse("69d2fad2-9ffe-4f2d-ad85-3ff4b277f805"),
                Nome = "Arthur",
                Email = "arthuraasantos@hotmail.com",
                Senha = Criptografia.Encrypt("admin"),
                Empresa = empresa,
                PerfilDeAcesso = perfil
            };

            var funcionarioMarlon = new Funcionario()
            {
                Id = Guid.Parse("26341447-4897-4d49-851d-25890888e463"),
                Nome = "Marlon",
                Email = "marlonvss@gmail.com",
                Senha = Criptografia.Encrypt("admin"),
                Empresa = empresa,
                PerfilDeAcesso = perfil
            };

            var funcionarioCharles = new Funcionario()
            {
                Id = Guid.Parse("73ee557e-bd03-469d-8ed4-034266e1e82f"),
                Nome = "Charles",
                Email = "charles.info@ymail.com",
                Senha = Criptografia.Encrypt("admin"),
                Empresa = empresa,
                PerfilDeAcesso = perfil
            };  

            var funcionarioTeste = new Funcionario()
            {
                Id = Guid.Parse("4eb58c13-5077-4e24-ba4c-8d0173ed3942"),
                Nome = "Usuário Teste",
                Email = "administrador@fabricatcc.com",
                Senha = Criptografia.Encrypt("admin"),
                Empresa = empresaRH,
                PerfilDeAcesso = perfilFuncionario
            };

            var funcionarioRH = new Funcionario()
            {
                Id = Guid.Parse("01e9ba15-272a-4871-8377-6d2f4e621a67"),
                Nome = "Usuário Teste RH",
                Email = "rh@fabricatcc.com",
                Senha = Criptografia.Encrypt("admin"),
                Empresa = empresaRH,
                PerfilDeAcesso = perfilRH
            };            

            context.Set<Funcionario>().Add(funcionario);
            context.Set<Funcionario>().Add(funcionarioMarlon);
            context.Set<Funcionario>().Add(funcionarioCharles);
            context.Set<Funcionario>().Add(funcionarioTeste);
            context.Set<Funcionario>().Add(funcionarioRH);

            #endregion
        }
    }
}
