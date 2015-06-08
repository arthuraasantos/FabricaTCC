using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{
    public class HorarioDeExpediente : EntityBase
    {
        public int NumeroHorasPorDia { get; set; }
        public string Descricao { get; set; }
        public Empresa Empresa { get; set; }
        
    }
}
