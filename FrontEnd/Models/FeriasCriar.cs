using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio.Model;
using Seedwork.Const;
using System.ComponentModel.DataAnnotations;
using Seedwork.Entity;

namespace FrontEnd.Models
{
    public class FeriasCriar : EntityModel
    {
        [Required]
        public string Funcionario { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Inicio { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fim { get; set; }

        [Required]
        public String Justificativa { get; set; }
        public TipoSolicitacao Tipo { get; set; }
        public RespostaSolicitacao Resposta { get; set; }

    }
}