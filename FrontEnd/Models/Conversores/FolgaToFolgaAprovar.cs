using Dominio.Model;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.Conversores
{
    public class FolgaToFolgaAprovar : ConversorBase<Folga, FolgaAprovar>
    {
        public override void AplicarValores(FolgaAprovar origem, Folga destino)
        {
            throw new NotImplementedException();
        }
        public override void AplicarValores(Folga origem, FolgaAprovar destino)
        {
            throw new NotImplementedException();
        }
    }
}