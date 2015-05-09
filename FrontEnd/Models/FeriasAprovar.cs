using Dominio.Model;
using Seedwork.Const;
using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class FeriasAprovar : EntityModel
    {
        public virtual Funcionario Funcionario { get; set; }
        public DateTime Inicio { get; set; }

        public DateTime Fim { get; set; }
        public String Justificativa { get; set; }
        public TipoSolicitacao Tipo { get; set; }

        [Required]
        public RespostaSolicitacao Resposta { get; set; }
    }
}