using Dominio.Model;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.Conversores
{
    public class PontoToPontoAjustar : ConversorBase<Ponto, PontoEditar>
    {
        public override void AplicarValores(Ponto origem, PontoEditar destino)
        {
            destino.DataAjuste = origem.DataAjuste;
            destino.MotivoAjuste = origem.MotivoAjuste;
            destino.AjusteAprovado = origem.AjusteAprovado;
        }

        public override void AplicarValores(PontoEditar origem, Ponto destino)
        {
            destino.DataAjuste = origem.DataAjuste;
            destino.MotivoAjuste = origem.MotivoAjuste;
            destino.AjusteAprovado = origem.AjusteAprovado;
        }
    }
}