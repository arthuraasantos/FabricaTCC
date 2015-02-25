using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class FuncionarioComHorasTrabalhadas
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }

        public TimeSpan HorasTrabalhadas { get; set; }
    }
}