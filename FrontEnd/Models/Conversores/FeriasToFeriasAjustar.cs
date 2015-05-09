using Dominio.Model;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.Conversores
{
    public class FeriasToFeriasAjustar : ConversorBase<Ferias, FeriasAprovar>
    {
        public override void AplicarValores(Ferias origem, FeriasAprovar destino)
        {
            destino.Inicio = origem.Inicio;
            destino.Fim = origem.Fim;
            destino.Funcionario = origem.Funcionario;
            destino.Justificativa = origem.Justificativa;
            destino.Resposta = origem.Resposta;
            destino.Tipo = origem.Tipo;
        }

        public override void AplicarValores(FeriasAprovar origem, Ferias destino)
        {
            destino.Inicio = origem.Inicio;
            destino.Fim = origem.Fim;
            destino.Funcionario = origem.Funcionario;
            destino.Justificativa = origem.Justificativa;
            destino.Resposta = origem.Resposta;
            destino.Tipo = origem.Tipo;
        }
    }
}