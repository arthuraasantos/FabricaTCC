using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Seedwork.Conversores;
using Dominio.Model;

namespace FrontEnd.Models.Conversores
{
    public class EmpresaToEmpresaNovo : ConversorBase<Empresa, EmpresaNovo>
    {
        public override void AplicarValores(Empresa origem, EmpresaNovo destino)
        {
            destino.RazaoSocial = origem.RazaoSocial;
            destino.Cnpj = origem.Cnpj;
            destino.NomeFantasia = origem.NomeFantasia;
        }
        public override void AplicarValores(EmpresaNovo origem, Empresa destino)
        {
            destino.RazaoSocial = origem.RazaoSocial;
            destino.Cnpj = origem.Cnpj;
            destino.NomeFantasia = origem.NomeFantasia;
        }
    }
}