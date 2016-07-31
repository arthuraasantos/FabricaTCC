using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{
    public class ItemHorarioDeExpediente : EntityBase
    {
        public int Horas { get; set; }
        public int DiaSemana { get; set; }
        public virtual HorarioDeExpediente HorarioDeExpediente { get; set; }
    }
}
