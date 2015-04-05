using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class PontoEditar : EntityModel
    {

        [Description("Data do Ajuste")]
        public string DataAjuste { get; set; }

        [Description("Hora do Ajuste")]
        public string HoraAjuste { get; set; }

        [Description("Motivo do Ajuste")]
        public String MotivoAjuste { get; set; }

    }
}