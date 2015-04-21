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
    public class SolicitacaoAprovar : EntityModel
    {
        public virtual Ponto Ponto { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public DateTime DataHora { get; set; }
        public String Justificativa { get; set; }
        public TipoSolicitacao Tipo { get; set; }
        [Required]
        public RespostaSolicitacao Resposta { get; set; }
    }
}