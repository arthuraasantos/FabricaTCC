using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Seedwork.Conversores;
using Dominio.Model;

namespace FrontEnd.Models.Conversores
{
    public class PerfilDeAcessoToPerfilDeAcessoNovo : ConversorBase<PerfilDeAcesso, PerfilDeAcessoNovo>
    {
        public override void AplicarValores(PerfilDeAcesso origem, PerfilDeAcessoNovo destino)
        {
            destino.Descricao= origem.Descricao;
        }

        public override void AplicarValores(PerfilDeAcessoNovo origem, PerfilDeAcesso destino)
        {
            destino.Descricao = origem.Descricao;
        }
    }
}