using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using Infraestrutura;
using Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{

    public class FolhaPontoController : Controller
    {
        private IFolhaPontoRepository FolhaPontoRepository { get; set; }
        private IEmpresaRepository EmpresaRepository { get; set; }

        public FolhaPontoController(MyContext context, IFolhaPontoRepository folhaPontoRepository, IEmpresaRepository empresaRepository)
        {
            FolhaPontoRepository = folhaPontoRepository;
            EmpresaRepository = empresaRepository;
        }

        public ActionResult Index()
        {

            var lista = FolhaPontoRepository.
                            Listar().
                            Where(p => p.Empresa.Id == Sessao.EmpresaLogada.Id).
                            OrderByDescending(p => p.DataFim).
                            ToList();


            return View(lista);
        }

    }
}