using Seedwork.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class FeriasCriar : EntityModel
    {
        [Required]
        public string Funcionario { get; set; }

        [Required(ErrorMessage = "A data de início deve ser informada!")]
        public DateTime Inicio { get; set; }

        [Required(ErrorMessage = "A data final das férias deve ser informada!")]


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fim { get; set; }
        public string Justificativa { get; set; }

        public TipoSolicitacao Tipo { get; set; }
        public RespostaSolicitacao Resposta { get; set; }
    }
}