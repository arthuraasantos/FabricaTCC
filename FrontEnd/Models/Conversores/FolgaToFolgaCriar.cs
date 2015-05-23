using Dominio.Model;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.Conversores
{
    public class FolgaToFolgaCriar : ConversorBase<Folga, FolgaCriar>
    {
        public override void AplicarValores(Folga origem, FolgaCriar destino)
        {
            destino.Funcionario = origem.Funcionario.Email;
            destino.Data = origem.Data;
            destino.Justificativa = origem.Justificativa;
            destino.Resposta = origem.Resposta;
        }

        public override void AplicarValores(FolgaCriar origem, Folga destino)
        {
            destino.Id = Guid.NewGuid();
            destino.Data = origem.Data;
            destino.Justificativa = origem.Justificativa;
            destino.Resposta = origem.Resposta;
        }
    }
}