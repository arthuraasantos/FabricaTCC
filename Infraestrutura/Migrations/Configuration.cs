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
                Descricao = "Administrador",
                Criacao = DateTime.Now.Date
            };

            var perfilGerente = new PerfilDeAcesso()
            {
                Id = Guid.Parse("4036b285-e7fb-4ac0-aac4-5196fde05dd8"),
                Descricao = "Gerente/RH",
                Criacao = DateTime.Now.Date
            };

            var perfilFuncionario = new PerfilDeAcesso()
            {
                Id = Guid.Parse("3acc5625-0663-47c2-bb4d-e507ee5b004f"),
                Descricao = "Funcionario",
                Criacao = DateTime.Now.Date
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
                Logradouro = "Estrada do Campinho",
                Criacao = DateTime.Now.Date

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
                Criacao = DateTime.Now.Date

            };
            var horario = context.Set<HorarioDeExpediente>().FirstOrDefault(p => p.Id == horarioPadrao.Id);
            if (horario == null) { context.Set<HorarioDeExpediente>().Add(horarioPadrao); }

            #region 'Itens de Hora de expediente 0'

            ItemHorarioDeExpediente itemHorarioPadrao0 = new ItemHorarioDeExpediente()
            {
                Id = Guid.Parse("cd03e454-4316-43fd-96cf-3ef1b91c60a5"),
                DiaSemana = 0,
                Horas = 0,
                HorarioDeExpediente = horarioPadrao,
                Criacao = DateTime.Now.Date
            };

            var ItemHorario0 = context.Set<ItemHorarioDeExpediente>().FirstOrDefault(p => p.Id == itemHorarioPadrao0.Id);
            if (ItemHorario0 == null) { context.Set<ItemHorarioDeExpediente>().Add(itemHorarioPadrao0); }

            #endregion
            #region 'Itens de Hora de expediente 1'

            ItemHorarioDeExpediente itemHorarioPadrao1 = new ItemHorarioDeExpediente()
            {
                Id = Guid.Parse("cd03e454-4316-43fd-96cf-3ef1b91c60a6"),
                DiaSemana = 1,
                Horas = 8,
                HorarioDeExpediente = horarioPadrao,
                Criacao = DateTime.Now.Date
            };


            var ItemHorario1 = context.Set<ItemHorarioDeExpediente>().FirstOrDefault(p => p.Id == itemHorarioPadrao1.Id);
            if (ItemHorario1 == null) { context.Set<ItemHorarioDeExpediente>().Add(itemHorarioPadrao1); }

            #endregion
            #region 'Itens de Hora de expediente 2'

            ItemHorarioDeExpediente itemHorarioPadrao2 = new ItemHorarioDeExpediente()
            {
                Id = Guid.Parse("cd03e454-4316-43fd-96cf-3ef1b91c60a7"),
                DiaSemana = 2,
                Horas = 8,
                HorarioDeExpediente = horarioPadrao,
                Criacao = DateTime.Now.Date
            };


            var ItemHorario2 = context.Set<ItemHorarioDeExpediente>().FirstOrDefault(p => p.Id == itemHorarioPadrao2.Id);
            if (ItemHorario2 == null) { context.Set<ItemHorarioDeExpediente>().Add(itemHorarioPadrao2); }

            #endregion
            #region 'Itens de Hora de expediente 3'

            ItemHorarioDeExpediente itemHorarioPadrao3 = new ItemHorarioDeExpediente()
            {
                Id = Guid.Parse("cd03e454-4316-43fd-96cf-3ef1b91c60a8"),
                DiaSemana = 3,
                Horas = 8,
                HorarioDeExpediente = horarioPadrao,
                Criacao = DateTime.Now.Date
            };


            var ItemHorario3 = context.Set<ItemHorarioDeExpediente>().FirstOrDefault(p => p.Id == itemHorarioPadrao3.Id);
            if (ItemHorario3 == null) { context.Set<ItemHorarioDeExpediente>().Add(itemHorarioPadrao3); }

            #endregion
            #region 'Itens de Hora de expediente 4'

            ItemHorarioDeExpediente itemHorarioPadrao4 = new ItemHorarioDeExpediente()
            {
                Id = Guid.Parse("cd03e454-4316-43fd-96cf-3ef1b91c60a9"),
                DiaSemana = 4,
                Horas = 8,
                HorarioDeExpediente = horarioPadrao,
                Criacao = DateTime.Now.Date
            };


            var ItemHorario4 = context.Set<ItemHorarioDeExpediente>().FirstOrDefault(p => p.Id == itemHorarioPadrao4.Id);
            if (ItemHorario4 == null) { context.Set<ItemHorarioDeExpediente>().Add(itemHorarioPadrao4); }

            #endregion
            #region 'Itens de Hora de expediente 5'

            ItemHorarioDeExpediente itemHorarioPadrao5 = new ItemHorarioDeExpediente()
            {
                Id = Guid.Parse("cd03e454-4316-43fd-96cf-3ef1b91c6010"),
                DiaSemana = 5,
                Horas = 8,
                HorarioDeExpediente = horarioPadrao,
                Criacao = DateTime.Now.Date
            };


            var ItemHorario5 = context.Set<ItemHorarioDeExpediente>().FirstOrDefault(p => p.Id == itemHorarioPadrao5.Id);
            if (ItemHorario5 == null) { context.Set<ItemHorarioDeExpediente>().Add(itemHorarioPadrao5); }

            #endregion
            #region 'Itens de Hora de expediente 6'

            ItemHorarioDeExpediente itemHorarioPadrao6 = new ItemHorarioDeExpediente()
            {
                Id = Guid.Parse("cd03e454-4316-43fd-96cf-3ef1b91c6011"),
                DiaSemana = 6,
                Horas = 0,
                HorarioDeExpediente = horarioPadrao,
                Criacao = DateTime.Now.Date
            };


            var ItemHorario6 = context.Set<ItemHorarioDeExpediente>().FirstOrDefault(p => p.Id == itemHorarioPadrao6.Id);
            if (ItemHorario6 == null) { context.Set<ItemHorarioDeExpediente>().Add(itemHorarioPadrao6); }

            #endregion

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
                HorarioDeExpediente = horarioPadrao,
                Criacao = DateTime.Now.Date
            };


            var func = context.Set<Funcionario>().FirstOrDefault(p => p.Email == funcionarioAdministrador.Email);
            if (func == null) { context.Set<Funcionario>().Add(funcionarioAdministrador); }

            #endregion
        }
    }
}
