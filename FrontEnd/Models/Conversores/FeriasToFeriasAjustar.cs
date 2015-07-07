using Dominio.Model;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.Conversores
{
    public class FeriasToFeriasAjustar: ConversorBase<Ferias, FeriasAprovar>
    {
        public override void AplicarValores(Ferias origem, FeriasAprovar destino)
        {
            destino.Funcionario = origem.Funcionario.Email;
            destino.Inicio = origem.Inicio.Date;
            destino.Fim = origem.Fim.Date;
            destino.Tipo = origem.Tipo;
            destino.Resposta = origem.Resposta;
        }
        public override void AplicarValores(FeriasAprovar origem, Ferias destino)
        {
            destino.Funcionario.Email = origem.Funcionario;
            destino.Inicio = origem.Inicio.Date;
            destino.Fim = origem.Fim.Date;
            destino.Tipo = origem.Tipo;
            destino.Resposta = origem.Resposta;
        }
    }
}