using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using FrontEnd.Models.Conversores;
using Infraestrutura;
using Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class EmpresaController : BaseController<Empresa, EmpresaNovo, EmpresaEditar>
    {        
        public EmpresaController(MyContext context, IEmpresaRepository empresaRepository) :base(context, empresaRepository, new EmpresaToEmpresaNovo(), new EmpresaToEmpresaEditar())
        {
             
        }       
    }
}