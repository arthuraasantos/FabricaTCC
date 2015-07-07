using Dominio.Model;
using Dominio.Repository;
using Seedwork.Conversores;
using Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeedWork.Tools;

namespace FrontEnd.Models.Conversores
{
    public class FuncionarioToFuncionarioNovo : ConversorBase<Funcionario, FuncionarioNovo>
    {
        public IPerfilDeAcessoRepository PerfilDeAcessoRepository { get; set; }
        public IEmpresaRepository EmpresaRepository { get; set; }
        public IHorarioDeExpedienteRepository HorarioDeExpedienteRepository { get; set; }


        public FuncionarioToFuncionarioNovo(IPerfilDeAcessoRepository perfilDeAcessoRepository, IEmpresaRepository empresaRepository, IHorarioDeExpedienteRepository horarioDeExpedienteRepository)
        {
            EmpresaRepository = empresaRepository;
            PerfilDeAcessoRepository = perfilDeAcessoRepository;
            HorarioDeExpedienteRepository = horarioDeExpedienteRepository;
        }
        
        
        public override void AplicarValores(Funcionario origem, FuncionarioNovo destino)
        {
            destino.Nome = origem.Nome;
            destino.DataNascimento = origem.DataNascimento;
            destino.Cpf = origem.Cpf;
            destino.Identidade = origem.Identidade;
            destino.Email = origem.Email;
            destino.Senha = origem.Senha;
            destino.IdEmpresa = origem.Empresa.Id;
            destino.IdPerfilDeAcesso = origem.PerfilDeAcesso.Id;
            destino.IdHorarioDeExpediente = origem.HorarioDeExpediente.Id;
        }
        public override void AplicarValores(FuncionarioNovo origem, Funcionario destino)
        {                 
            destino.Nome = origem.Nome;
            destino.DataNascimento = origem.DataNascimento;
            destino.Cpf = origem.Cpf;
            destino.Identidade = origem.Identidade;
            destino.Email = origem.Email;
            destino.Senha = Criptografia.Encrypt(origem.Senha);
            destino.Empresa = EmpresaRepository.PesquisarPeloId(origem.IdEmpresa);
            destino.PerfilDeAcesso = PerfilDeAcessoRepository.PesquisarPeloId(origem.IdPerfilDeAcesso);
            destino.HorarioDeExpediente = HorarioDeExpedienteRepository.PesquisarPeloId(origem.IdHorarioDeExpediente);
        }
    }
}