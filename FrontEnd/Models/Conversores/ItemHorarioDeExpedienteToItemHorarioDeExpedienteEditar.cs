using Dominio.Model;
using Dominio.Repository;
using FrontEnd.Models;
using Seedwork.Conversores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrontEnd.Controllers
{
    public class ItemHorarioDeExpedienteToItemHorarioDeExpedienteEditar
    {

        public void AplicarValores(List<ItemHorarioDeExpediente> origem, ItemHorarioDeExpedienteEditar destino)
        {
            foreach (ItemHorarioDeExpediente item in origem)
            {
                switch (item.DiaSemana)
                {
                    case 0:
                        destino.HorasDia0 = item.Horas;
                        break;
                    case 1:
                        destino.HorasDia1 = item.Horas;
                        break;
                    case 2:
                        destino.HorasDia2 = item.Horas;
                        break;
                    case 3:
                        destino.HorasDia3 = item.Horas;
                        break;
                    case 4:
                        destino.HorasDia4 = item.Horas;
                        break;
                    case 5:
                        destino.HorasDia5 = item.Horas;
                        break;
                    case 6:
                        destino.HorasDia6 = item.Horas;
                        break;
                }

                destino.IdHorarioDeExpediente = item.HorarioDeExpediente.Id;
            }

        }
        public void AplicarValores(ItemHorarioDeExpedienteEditar origem, List<ItemHorarioDeExpediente> destino)
        {
            foreach(ItemHorarioDeExpediente item in destino)
            {
                switch (item.DiaSemana)
                {
                    case 0:
                        item.Horas = origem.HorasDia0;
                        break;
                    case 1:
                        item.Horas = origem.HorasDia1;
                        break;
                    case 2:
                        item.Horas = origem.HorasDia2;
                        break;
                    case 3:
                        item.Horas = origem.HorasDia3;
                        break;
                    case 4:
                        item.Horas = origem.HorasDia4;
                        break;
                    case 5:
                        item.Horas = origem.HorasDia5;
                        break;
                    case 6:
                        item.Horas = origem.HorasDia6;
                        break;
                }
                item.Alteracao = DateTime.Now;
            }
        }
    }
}