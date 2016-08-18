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
    public class ItemHorarioDeExpedienteController : Controller
    {
        // GET: ItemHorarioDeExpediente
        public IItemHorarioDeExpedienteRepository ItemHorarioDeExpedienteRepository { get; set; }
        public IHorarioDeExpedienteRepository HorarioDeExpedienteRepository { get; set; }
        public PontoContext Context { get; set; }
        public ItemHorarioDeExpedienteToItemHorarioDeExpedienteEditar Converter { get; set; }
        public List<ItemHorarioDeExpediente> lista { get; set; }

        public ItemHorarioDeExpedienteController(PontoContext context)
        {
            Context = context;
            ItemHorarioDeExpedienteRepository = new ItemHorarioDeExpedienteRepository(context);
            HorarioDeExpedienteRepository = new HorarioDeExpedienteRepository(context);
            Converter = new ItemHorarioDeExpedienteToItemHorarioDeExpedienteEditar();
            lista = new List<ItemHorarioDeExpediente>();
        }

        public ActionResult Index(Guid idHorario)
        {
            lista = ItemHorarioDeExpedienteRepository.Listar().Where(f => f.HorarioDeExpediente.Id == idHorario).ToList();
            foreach (ItemHorarioDeExpediente item in lista)
            {
                item.HorarioDeExpediente = new HorarioDeExpediente()
                {
                    Id = idHorario
                };
            }

            ItemHorarioDeExpedienteEditar Item = new ItemHorarioDeExpedienteEditar();
            Converter.AplicarValores(lista, Item);

            return View("Index", Item);
        }

        public ActionResult Salvar(ItemHorarioDeExpedienteEditar Item)
        {
            lista = ItemHorarioDeExpedienteRepository.Listar().Where(f => f.HorarioDeExpediente.Id == Item.IdHorarioDeExpediente).ToList();
            Converter.AplicarValores(Item, lista);

            foreach (ItemHorarioDeExpediente modelo in lista)
            {
                ItemHorarioDeExpedienteRepository.Salvar(modelo);
            }
            Context.SaveChanges();

            return RedirectToAction("Index", "HorarioDeExpediente");
        }
    }
}