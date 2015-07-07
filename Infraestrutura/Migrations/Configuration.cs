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
            
            var perfilAdminitrador = new PerfilDeAcesso()
            {
                Id = Guid.Parse("09c15773-d1a5-4daa-a3b3-e8c6741d63fd"),
                Descricao = "Administrador"
            };

            var perfilGerente = new PerfilDeAcesso()
            {
                Id = Guid.Parse("4036b285-e7fb-4ac0-aac4-5196fde05dd8"),
                Descricao = "Gerente/RH"
            };

            var perfilFuncionario = new PerfilDeAcesso()
            {
                Id = Guid.Parse("3acc5625-0663-47c2-bb4d-e507ee5b004f"),
                Descricao = "Funcionario"
            };

            var perf = context.Set<PerfilDeAcesso>().FirstOrDefault(p => p.Id == perfilAdminitrador.Id);
            if (perf == null) { context.Set<PerfilDeAcesso>().Add(perfilAdminitrador); }

            perf = context.Set<PerfilDeAcesso>().FirstOrDefault(p => p.Id == perfilGerente.Id);
            if (perf == null) { context.Set<PerfilDeAcesso>().Add(perfilGerente); }

            perf = context.Set<PerfilDeAcesso>().FirstOrDefault(p => p.Id == perfilFuncionario.Id);
            if (perf == null) { context.Set<PerfilDeAcesso>().Add(perfilFuncionario); }

            #endregion

            #region 'Empresas'
            
            var empresa = new Empresa()
            {
                Id = Guid.Parse("e6a1d40d-05bf-4a81-b1fd-72893c07a23e"),
                RazaoSocial = "FabricaTCC",
                Bairro = "Campo Grande",
                Bloqueado = "N",
                Cep = "23070420",
                Cidade = "Rio de Janeiro",
                Cnpj = "012392834000198",
                Estado = "RJ", 
                NomeFantasia = "Fabrica TCC",
                NumeroEndereco = 1330,
                Pais = "Brasil",
                Logradouro = "Estrada do Campinho"
                
            };

            var emp = context.Set<Empresa>().FirstOrDefault(p => p.Id == empresa.Id);
            if (emp == null) { context.Set<Empresa>().Add(empresa); }

            #endregion

            #region 'Horarios de Expediente'

            HorarioDeExpediente horarioPadrao = new HorarioDeExpediente()
            {
                Id = Guid.Parse("182E32E3-1546-435F-9F00-D509C3161C95"),
                Empresa = empresa,
                Descricao = "Horário Padrão",
                NumeroHorasPorDia = 8
            };
            var horario = context.Set<HorarioDeExpediente>().FirstOrDefault(p => p.Id == horarioPadrao.Id);
            if (horario == null) { context.Set<HorarioDeExpediente>().Add(horarioPadrao); }

            #endregion

            #region 'Funcionários'

            var funcionarioAdministrador = new Funcionario()
            {
                Id = Guid.Parse("69d2fad2-9ffe-4f2d-ad85-3ff4b277f805"),
                Nome = "Administrador",
                Email = "administrador@fabricatcc.com",
                Senha = Criptografia.Encrypt("admin"),
                Empresa = empresa,
                PerfilDeAcesso = perfilAdminitrador,
                HorarioDeExpediente = horarioPadrao
            };


            var func = context.Set<Funcionario>().FirstOrDefault(p => p.Email == funcionarioAdministrador.Email);
            if (func == null) { context.Set<Funcionario>().Add(funcionarioAdministrador); }

            #endregion
        }
    }
}
