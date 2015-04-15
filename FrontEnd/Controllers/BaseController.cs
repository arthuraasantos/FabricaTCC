using Infraestrutura;
using Seedwork.Entity;
using Seedwork.Repository;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEnd.Models;
using Dominio.Model;

namespace FrontEnd.Controllers
{
    /// <summary>
    /// Sempre será necessario 3 views para a controller base funcionar, Uma Index (Listagem), Uma Vizualição (Visualizar) e Um NovoRegistro (Novo)
    /// </summary>
    /// <typeparam name="TEntidade"></typeparam>
    /// <typeparam name="TInsertModel"></typeparam>
    /// <typeparam name="TEditModel"></typeparam>
    public abstract class BaseController<TEntidade, TInsertModel, TEditModel> : Controller
        where TEntidade : EntityBase
        where TInsertModel : EntityModel
        where TEditModel : EntityModel
    {

        public MyContext Context { get; set; }
        public IRepository<TEntidade> Repository { get; set; }
        public IConversor<TEntidade, TInsertModel> ConversorInsert { get; set; }
        public IConversor<TEntidade, TEditModel> ConversorEdit { get; set; }
        public BaseController(MyContext context, IRepository<TEntidade> repository, IConversor<TEntidade, TInsertModel> conversorInsert, IConversor<TEntidade, TEditModel> conversorEdit)
        {
            Context = context;
            Repository = repository;
            ConversorEdit = conversorEdit;
            ConversorInsert = conversorInsert;
        }

        public virtual ActionResult Index()
        {
            
            var lista = Repository.Listar().ToList();
            return View("Index", lista);
        }
        public virtual ActionResult Visualizar(Guid Id)
        {

            var entidade =
                 Repository.PesquisarPeloId(Id);

            var modelEditar = ConversorEdit.Converter(entidade);

            return View("Visualizar", modelEditar);
        }

        public virtual ActionResult Novo()
        {
            return View("Novo", Activator.CreateInstance<TInsertModel>());
        }

        public virtual ActionResult Incluir(TInsertModel novo)
        {             
            var entity = ConversorInsert.Converter(novo);
            entity.Id = Guid.NewGuid();

            Repository.Salvar(entity);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }
        public virtual ActionResult Editar(TEditModel editar)
        {
            
            var entity = Repository.PesquisarPeloId(editar.Id);

            ConversorEdit.AplicarValores(editar, entity);

            Repository.Salvar(entity);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public virtual ActionResult Excluir(Guid Id)
        {
            var entity =
                Repository.PesquisarPeloId(Id);
            Repository.Remover(entity);
            Context.SaveChanges();
            ViewBag.Mensagem = "Excluído com sucesso!";
            return RedirectToAction("Index");
        }
    }
}