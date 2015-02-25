using Dominio.Model;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.Conversores
{
    public class FuncionarioToFuncionarioNovo : ConversorBase<Funcionario, FuncionarioNovo>
    {
        public override void AplicarValores(Funcionario origem, FuncionarioNovo destino)
        {
            destino.Nome = origem.Nome;
            destino.DataNascimento = origem.DataNascimento;
            destino.Cpf = origem.Cpf;
            destino.Identidade = origem.Identidade;

        }

        public override void AplicarValores(FuncionarioNovo origem, Funcionario destino)
        {
            origem.Nome = destino.Nome;
            origem.DataNascimento = destino.DataNascimento;
            origem.Cpf = destino.Cpf;
            origem.Identidade = destino.Identidade;
        }
    }
}