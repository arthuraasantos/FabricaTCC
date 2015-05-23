using Seedwork.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class FolgaAprovar: EntityModel
    {
        [Required]
        public string Funcionario { get; set; }

        [Required]
        public DateTime Data { get; set; }

        public string Justificativa { get; set; }

        public RespostaSolicitacao Resposta { get; set; }

    }
}