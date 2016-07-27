using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCCPontoEletronico.AppService.Interface;

namespace FrontEnd.Controllers
{
    public class ImportacaoController : Controller
    {
        private readonly IFuncionarioService _funcionarioService;

        public ImportacaoController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }
        // GET: Importacao

        [HttpPost]
        public JsonResult ImportarFuncionarios(List<string[]> textoPlanilha, string[] relacaoColunas)
        {
            try
            {
                _funcionarioService.ImportarTextoPlanilha(textoPlanilha, relacaoColunas, Seedwork.Const.TipoImportacao.Funcionario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Json(null,JsonRequestBehavior.AllowGet);
        }
    }
}