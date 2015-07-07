using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Seedwork.Conversores;
using Dominio.Model;

namespace FrontEnd.Models.Conversores
{
    public class PerfilDeAcessoToPerfilDeAcessoEditar : ConversorBase<PerfilDeAcesso, PerfilDeAcessoEditar>
    {
        public override void AplicarValores(PerfilDeAcesso origem, PerfilDeAcessoEditar destino)
        {
            destino.Descricao= origem.Descricao;
        }
        public override void AplicarValores(PerfilDeAcessoEditar origem, PerfilDeAcesso destino)
        {
            destino.Descricao = origem.Descricao;
        }
    }
}