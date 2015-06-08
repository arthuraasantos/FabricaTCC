using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrontEnd.Models
{
    public class HorarioDeExpedienteEditar: EntityModel
    {
        public int NumeroHorasPorDia { get; set; }
        public string Descricao { get; set; }
        public Guid IdEmpresa { get; set; }

    }
}
