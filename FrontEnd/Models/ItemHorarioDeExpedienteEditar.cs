using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrontEnd.Models
{
    public class ItemHorarioDeExpedienteEditar: EntityModel
    {
        public Guid IdHorarioDeExpediente { get; set; }
        public int HorasDia0 { get; set; }
        public int HorasDia1 { get; set; }
        public int HorasDia2 { get; set; }
        public int HorasDia3 { get; set; }
        public int HorasDia4 { get; set; }
        public int HorasDia5 { get; set; }
        public int HorasDia6 { get; set; }
    }
}
