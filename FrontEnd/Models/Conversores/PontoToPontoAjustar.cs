using Dominio.Model;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Seedwork.Entity;
using Seedwork.Const;

namespace FrontEnd.Models.Conversores
{
    public class PontoToPontoAjustar : ConversorBase<Ponto, PontoEditar>
    {
        public override void AplicarValores(Ponto origem, PontoEditar destino)
        {
            //destino.DataAjuste = String.Format("{0: dd/MM/yyyy}", origem.DataValida.GetValueOrDefault());
            //destino.HoraAjuste = String.Format("{0: HH:mm}", origem.DataValida.GetValueOrDefault());
            //destino.Justificativa = origem.Justificativa;
        }

        public override void AplicarValores(PontoEditar origem, Ponto destino)
        {
            //destino.DataAjuste = DateTime.Parse(origem.DataAjuste + " " + origem.HoraAjuste);
            //destino.Justificativa = origem.Justificativa;
            //destino.Status = StatusPonto.Nenhum;
        }
    }
}