using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class PontoEditar : EntityModel
    {

        [Description("Data Marcada")]
        public DateTime DataDaMarcacao { get; set; }

        [Description("Data do Ajuste")]
        public DateTime DataAjuste { get; set; }

        [Description("Motivo do Ajuste")]
        public String MotivoAjuste { get; set; }

        [Description("Status")]
        public Boolean AjusteAprovado { get; set; }

    }
}