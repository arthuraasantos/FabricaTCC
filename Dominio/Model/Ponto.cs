﻿using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{
    public class Ponto : EntityBase
    {
        public virtual Funcionario Funcionario { get; set; }
        public DateTime DataDaMarcacao { get; set; }
        public DateTime DataAjuste { get; set; }
        public String MotivoAjuste { get; set; }
        public Boolean AjusteAprovado { get; set; }
    }
}
