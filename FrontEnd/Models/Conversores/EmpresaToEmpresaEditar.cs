using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Seedwork.Conversores;
using Dominio.Model;

namespace FrontEnd.Models.Conversores
{
    public class EmpresaToEmpresaEditar : ConversorBase<Empresa, EmpresaEditar>
    {
        public override void AplicarValores(Empresa origem, EmpresaEditar destino)
        {
            destino.RazaoSocial = origem.RazaoSocial;
            destino.NomeFantasia = origem.NomeFantasia;
            destino.Cnpj = origem.Cnpj;
            destino.Logradouro = origem.Logradouro;
            destino.NumeroEndereco = origem.NumeroEndereco;
            destino.Bairro = origem.Bairro;
            destino.Cidade = origem.Cidade;
            destino.Estado = origem.Estado;
            if (origem.Bloqueado == "N"){
                destino.Bloqueado = false;
            }
            else
                destino.Bloqueado = true;
        }

        public override void AplicarValores(EmpresaEditar origem, Empresa destino)
        {
            destino.RazaoSocial = origem.RazaoSocial;
            destino.NomeFantasia = origem.NomeFantasia;
            destino.Cnpj = origem.Cnpj;
            destino.Logradouro = origem.Logradouro;
            destino.NumeroEndereco = origem.NumeroEndereco;
            destino.Bairro = origem.Bairro;
            destino.Cidade = origem.Cidade;
            destino.Cep = origem.Cep;
            destino.Estado = origem.Estado;
            destino.Pais = origem.Pais;
            if (origem.Bloqueado == false)
            {
                destino.Bloqueado = "N";
            }
            else
                destino.Bloqueado = "Y";

        }
    }
}

