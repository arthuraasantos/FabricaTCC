using Dominio.Model;
using Dominio.Repository;
using Seedwork.Conversores;
using Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.Conversores
{
    public class FuncionarioToFuncionarioNovo : ConversorBase<Funcionario, FuncionarioNovo>
    {
        public IPerfilDeAcessoRepository PerfilDeAcessoRepository { get; set; }
        public IEmpresaRepository EmpresaRepository { get; set; }
        public FuncionarioToFuncionarioNovo(IPerfilDeAcessoRepository perfilDeAcessoRepository, IEmpresaRepository empresaRepository)
        {
            EmpresaRepository = empresaRepository;
            PerfilDeAcessoRepository = perfilDeAcessoRepository;
        }
        public override void AplicarValores(Funcionario origem, FuncionarioNovo destino)
        {
            destino.Nome = origem.Nome;
            destino.DataNascimento = origem.DataNascimento;
            destino.Cpf = origem.Cpf;
            destino.Identidade = origem.Identidade;
            destino.Email = origem.Email;
            destino.IdEmpresa = origem.Empresa.Id;
            destino.IdPerfilDeAcesso = origem.PerfilDeAcesso.Id;
        }

        public override void AplicarValores(FuncionarioNovo origem, Funcionario destino)
        {                 
            destino.Nome = origem.Nome;
            destino.DataNascimento = origem.DataNascimento;
            destino.Cpf = origem.Cpf;
            destino.Identidade = origem.Identidade;
            destino.Email = origem.Email;
            destino.Empresa = EmpresaRepository.PesquisarPeloId(origem.IdEmpresa);
            destino.PerfilDeAcesso = PerfilDeAcessoRepository.PesquisarPeloId(origem.IdPerfilDeAcesso);
        }
    }
}