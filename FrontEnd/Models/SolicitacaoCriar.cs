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
    public class SolicitacaoCriar : EntityModel
    {
        public Guid Ponto { get; set; }
        [Required]
        public string Funcionario { get; set; }
        [Required]
        public String Data { get; set; }
        [Required]
        public String Hora { get; set; }
        [Required]
        public String Justificativa { get; set; }
        public TipoSolicitacao Tipo { get; set; }
        public RespostaSolicitacao Resposta { get; set; }
    }
}