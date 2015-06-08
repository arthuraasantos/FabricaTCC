using Dominio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models
{
    public class HorarioDeExpedienteNovo: EntityModel
    {
        public int NumeroHorasPorDia { get; set; }
        public string Descricao { get; set; }
        public Guid? IdEmpresa { get; set; }

        public IEnumerable<SelectListItem> Empresas { get; set; }




    }
}