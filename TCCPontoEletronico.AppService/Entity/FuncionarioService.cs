
using Dominio.Model;
using Dominio.Repository;
using Seedwork.Const;
using System;
using TCCPontoEletronico.AppService.Interface;
using TCCPontoEletronico.AppService.Interface.DTOs;
using TCCPontoEletronico.AppService.Static;
using System.Collections.Generic;
using Infraestrutura.Importacao;
using SeedWork.Tools;

namespace TCCPontoEletronico.AppService.Entity
{
    public class FuncionarioService : IFuncionarioService, IImportacaoService
    {
        private readonly Funcionario _funcionario;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IHorarioDeExpedienteRepository _horarioDeExpedienteRepository;
        private readonly IPerfilDeAcessoRepository _perfilDeAcessoRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository, IEmpresaRepository empresaRepository, IHorarioDeExpedienteRepository horarioDeExpedienteRepository,
                                  IPerfilDeAcessoRepository perfilDeAcessoRepository)
        {
            _funcionario = (Funcionario)System.Web.HttpContext.Current.Session["Funcionario"];
            _funcionarioRepository = funcionarioRepository;
            _empresaRepository = empresaRepository;
            _horarioDeExpedienteRepository = horarioDeExpedienteRepository;
            _perfilDeAcessoRepository = perfilDeAcessoRepository;
        }

        public string GetEmailEmployeeLogged() => _funcionario.Email;

        public string GetNameEmployeeLogged() => _funcionario.Nome;

        public string GetOrganizationNameLogged() => _funcionario.Empresa.NomeFantasia;

        public string GetAccessProfileDescription() => _funcionario.PerfilDeAcesso.Descricao;

        public Funcionario GetEmployeeLogged() => _funcionario;

        public PerfilAcesso GetAccessProfile()
        {
            switch (GetAccessProfileDescription())
            {
                case "Gerente/RH":
                    return PerfilAcesso.Gerente;
                case "Administrador":
                    return PerfilAcesso.Administrador;
                default:
                    return PerfilAcesso.Funcionario;
            }
        }

        public Guid GetOrganizationIdLogged() => _funcionario.Empresa.Id;

        public int GetCountEmployee() => _funcionarioRepository.GetCount();

        public FuncionarioDto CriarFuncionario(string nomeFuncionario, string emailFuncionario, Guid idEmpresa, Guid idHorarioExpediente, string senha)
        {
            try
            {
                Funcionario funcionario = new Funcionario();
                funcionario.Id = Guid.NewGuid();
                funcionario.Nome = nomeFuncionario;
                funcionario.Email = emailFuncionario;
                funcionario.Senha = Criptografia.Encrypt(senha);
                funcionario.PerfilDeAcesso = new PerfilDeAcesso();
                funcionario.PerfilDeAcesso = _perfilDeAcessoRepository.PesquisarPeloId(AccessProfile.GetManagerId());
                funcionario.HorarioDeExpediente = new HorarioDeExpediente();
                funcionario.HorarioDeExpediente = _horarioDeExpedienteRepository.PesquisarPeloId(idHorarioExpediente);
                funcionario.Empresa = new Empresa();
                funcionario.Empresa = _empresaRepository.PesquisarPeloId(idEmpresa);

                _funcionarioRepository.Salvar(funcionario);
                return new FuncionarioDto
                {
                    Id = funcionario.Id,
                    Name = funcionario.Nome,
                    Email = funcionario.Email,
                    OrganizationFantasyName = funcionario.Empresa.NomeFantasia,
                    Password = funcionario.Senha
                };
            }
            catch (Exception ex)
            {
                //ToDo Implementar log de erro
                throw;
            }
        }

        public bool ImportarTextoPlanilha(List<string[]> textoPlanilha, string[] relacaoColunas, TipoImportacao tipoImportacao)
        {
            var importacao = new ImportacaoFuncionario(_funcionarioRepository);
            importacao.ImportarTextoPlanilha(textoPlanilha,relacaoColunas,tipoImportacao);
            return true;
        }
    }
}
