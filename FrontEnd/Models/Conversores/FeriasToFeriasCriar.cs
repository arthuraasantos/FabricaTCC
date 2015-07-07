using Dominio.Model;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.Conversores
{
    public class FeriasToFeriasCriar: ConversorBase<Ferias,FeriasCriar>
    {

        public override void AplicarValores(Ferias origem, FeriasCriar destino)
        {
            destino.Funcionario = origem.Funcionario.Email;
            destino.Inicio = origem.Inicio;
            destino.Fim = origem.Fim;
            destino.Tipo = origem.Tipo;
            destino.Justificativa = origem.Justificativa;
            destino.Resposta = origem.Resposta;
        }
        public override void AplicarValores(FeriasCriar origem, Ferias destino)
        {
            destino.Id = Guid.NewGuid();
            destino.Inicio = origem.Inicio;
            destino.Fim = origem.Fim;
            destino.Justificativa = origem.Justificativa;
            destino.Tipo = origem.Tipo;
            destino.Resposta = origem.Resposta;
        }
    }
}