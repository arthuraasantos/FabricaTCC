using Dominio.Model;
using Dominio.Repository;
using Seedwork.Conversores;
using SeedWork.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.Conversores
{
    public class FuncionarioToFuncionarioEditar : ConversorBase<Funcionario, FuncionarioEditar>
    {
        public IEmpresaRepository EmpresaRepository;
        public IPerfilDeAcessoRepository PerfilDeAcesspRepository;
        public FuncionarioToFuncionarioEditar(IEmpresaRepository empresaRepository, IPerfilDeAcessoRepository perfilDeAcesspRepository)
        {
            EmpresaRepository = empresaRepository;
            PerfilDeAcesspRepository = perfilDeAcesspRepository;
        }
        public override void AplicarValores(Funcionario origem, FuncionarioEditar destino)
        {
            destino.Senha = origem.Senha.ToString();
            destino.SalarioBase = origem.SalarioBase;
            destino.Logradouro = origem.Logradouro;
            destino.Pais = origem.Pais;
            destino.NumeroEndereco = origem.NumeroEndereco;
            destino.Nome = origem.Nome;
            destino.Identidade = origem.Identidade;
            destino.Estado = origem.Estado;
            destino.IdEmpresa = origem.Empresa.Id;
            destino.IdPerfilDeAcesso = origem.PerfilDeAcesso.Id; 
            destino.Email = origem.Email;
            destino.DataNascimento = origem.DataNascimento;
            destino.Cpf = origem.Cpf;
            destino.Cidade = origem.Cidade;
            destino.Cep = origem.Cep;
            destino.Bairro = origem.Bairro;

        }

        public override void AplicarValores(FuncionarioEditar origem, Funcionario destino)
        {
            destino.Senha = Criptografia.Encrypt(origem.Senha);
            destino.SalarioBase = origem.SalarioBase;
            destino.Logradouro = origem.Logradouro;
            destino.Pais = origem.Pais;
            destino.NumeroEndereco = origem.NumeroEndereco;
            destino.Nome = origem.Nome;
            destino.Identidade = origem.Identidade;
            destino.Estado = origem.Estado;
            destino.Empresa = EmpresaRepository.PesquisarPeloId(origem.IdEmpresa);
            destino.PerfilDeAcesso = PerfilDeAcesspRepository.PesquisarPeloId(origem.IdPerfilDeAcesso);
            destino.Email = origem.Email;
            destino.DataNascimento = origem.DataNascimento;
            destino.Cpf = origem.Cpf;
            destino.Cidade = origem.Cidade;
            destino.Cep = origem.Cep;
            destino.Bairro = origem.Bairro;
        }
    }
}