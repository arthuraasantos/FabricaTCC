using Dominio.Model;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.Conversores
{
    public class SolicitacaoToSolicitacaoAjustar : ConversorBase<Solicitacao, SolicitacaoAprovar>
    {
        public override void AplicarValores(Solicitacao origem, SolicitacaoAprovar destino)
        {
            destino.DataHora = origem.DataHora;
            destino.Funcionario = origem.Funcionario;
            destino.Justificativa = origem.Justificativa;
            destino.Ponto = origem.Ponto;
            destino.Resposta = origem.Resposta;
            destino.Tipo = origem.Tipo;
        }

        public override void AplicarValores(SolicitacaoAprovar origem, Solicitacao destino)
        {
            destino.DataHora = origem.DataHora;
            destino.Funcionario = origem.Funcionario;
            destino.Justificativa = origem.Justificativa;
            destino.Ponto = origem.Ponto;
            destino.Resposta = origem.Resposta;
            destino.Tipo = origem.Tipo;
        }
    }
}