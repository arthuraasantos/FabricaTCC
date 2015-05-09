using Dominio.Model;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.Conversores
{
    public class FeriasToFeriasCriar : ConversorBase<Ferias, FeriasCriar> 
    {
        public override void AplicarValores(Ferias origem, FeriasCriar destino)
        {
            destino.Inicio = origem.Inicio.Date;
            destino.Funcionario = origem.Funcionario.Email;
            destino.Justificativa = origem.Justificativa;
            destino.Resposta = origem.Resposta;
            destino.Tipo = origem.Tipo;
        }

        public override void AplicarValores(FeriasCriar origem, Ferias destino)
        {
            destino.Id = Guid.NewGuid();
            destino.Inicio = origem.Inicio;
            destino.Justificativa = origem.Justificativa;
            destino.Resposta = origem.Resposta;
            destino.Tipo = origem.Tipo;
        }

    }
}