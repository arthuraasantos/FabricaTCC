using Dominio.Model;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.Conversores
{
    public class SolicitacaoToSolicitacaoCriar : ConversorBase<Solicitacao, SolicitacaoCriar> 
    {
        public override void AplicarValores(Solicitacao origem, SolicitacaoCriar destino)
        {
            destino.Hora = origem.DataHora.ToString("HH:mm");
            destino.Funcionario = origem.Funcionario.Email;
            destino.Justificativa = origem.Justificativa;
            destino.Ponto = origem.Ponto.Id;
            destino.Resposta = origem.Resposta;
            destino.Tipo = origem.Tipo;
        }

        public override void AplicarValores(SolicitacaoCriar origem, Solicitacao destino)
        {
            destino.Id = Guid.NewGuid();
            destino.DataHora = DateTime.Parse(origem.DataSolicitacao + " " + origem.Hora);

            destino.Justificativa = origem.Justificativa;
            destino.Resposta = origem.Resposta;
            destino.Tipo = origem.Tipo;
        }

    }
}