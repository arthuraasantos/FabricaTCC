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

namespace FrontEnd.Models
{
    public class PerfilDeAcessoController : BaseController<PerfilDeAcesso, PerfilDeAcessoNovo, PerfilDeAcessoEditar>
    {

        public PerfilDeAcessoController(PontoContext context, IPerfilDeAcessoRepository perfilDeAcessoRepository)
            : base(context, perfilDeAcessoRepository, new PerfilDeAcessoToPerfilDeAcessoNovo(), new PerfilDeAcessoToPerfilDeAcessoEditar())
        {
             
        }       


        public override ActionResult Excluir(Guid id)
        {
            ViewBag.Mensagem = "Excluido com sucesso!";
            
            return base.Excluir(id);
        }
    }
}