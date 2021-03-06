﻿using Dominio.Model;
using Dominio.Repository;
using System;
using System.Linq;
using System.Globalization;
using TCCPontoEletronico.AppService.Interface;
using System.Collections.Generic;

namespace TCCPontoEletronico.AppService.Entity
{
    public class PontoEletronicoService : IPontoEletronicoService
    {
        private string FormatoHora = "HH:mm";
        private readonly IPontoRepository PointRepository;
        private readonly IItemHorarioDeExpedienteRepository ItemHorarioDeExpedienteRepositorio;


        public PontoEletronicoService(IPontoRepository pointRepository, IItemHorarioDeExpedienteRepository itemHorarioDeExpedienteRepositorio)
        {
            PointRepository = pointRepository;
            ItemHorarioDeExpedienteRepositorio = itemHorarioDeExpedienteRepositorio;
        }

        public void EfetuarMarcacaoDePonto(Funcionario funcionario)
        {

            CultureInfo cult = new CultureInfo("pt-BR");
            string StrData = DateTime.UtcNow.AddHours(-3).ToString("dd/MM/yyyy HH:mm", cult);

            var novoPonto = new Ponto()
            {
                Id = Guid.NewGuid(),
                DataMarcacao = DateTime.Parse(StrData, cult),
                DataValida = DateTime.Parse(StrData, cult),
                Funcionario = funcionario,
                Contabilizar = true
            };

            PointRepository.Salvar(novoPonto);
            PointRepository.Executar();
        }

        public TimeSpan QuantidadeDeHorasTrabalhadasPorFuncionario(Funcionario funcionario, DateTime diaInicio, DateTime diaFinal, Boolean descontarHoras = true)
        {

            var _horaExpediente = 8;
            List<ItemHorarioDeExpediente> lista = new List<ItemHorarioDeExpediente>();
            if (funcionario.HorarioDeExpediente != null)
            {
                lista = ItemHorarioDeExpedienteRepositorio.Listar().Where(f => f.HorarioDeExpediente.Id == funcionario.HorarioDeExpediente.Id).ToList();
            }


            var _marcacoesDoDia = PointRepository.
                                Listar().
                                ToList().
                                Where(p => p.Funcionario.Id == funcionario.Id).
                                Where(p => p.DataValida.Date >= diaInicio.Date).
                                Where(p => p.DataValida.Date <= diaFinal.Date).
                                Where(p => p.Contabilizar == true).
                                OrderBy(p => p.DataValida).
                                ToList();

            int _Dias = diaFinal.Subtract(diaInicio).Days;
            var horasTrabalhadas = new TimeSpan();

            for (int i = 0; i <= _Dias; i++)
            {
                DateTime DataTeste = diaInicio.AddDays(i).Date;
                var _ListaPorDia = _marcacoesDoDia.Where(p => p.DataValida.Date == DataTeste).ToList();

                for (int j = 1; j < _ListaPorDia.Count; j = j + 2)
                {
                    horasTrabalhadas = horasTrabalhadas.Add(_ListaPorDia[j].DataValida.TimeOfDay - _ListaPorDia[j - 1].DataValida.TimeOfDay);
                }

                if ((_ListaPorDia.Count >= 4) && (descontarHoras))
                {
                    _horaExpediente = 8;
                    if (funcionario.HorarioDeExpediente != null)
                    {
                        foreach (ItemHorarioDeExpediente item in lista)
                        {
                            if (item.DiaSemana == (int)DataTeste.DayOfWeek)
                            {
                                _horaExpediente = item.Horas;
                                break;
                            }
                        }

                    }

                    horasTrabalhadas = horasTrabalhadas.Subtract(new TimeSpan(_horaExpediente, 0, 0));
                }

            }

            return horasTrabalhadas;
        }
        public TimeSpan QuantidadeDeHorasTrabalhadasPorFuncionarioPorDia(Funcionario funcionario, DateTime dia)
        {
            return this.QuantidadeDeHorasTrabalhadasPorFuncionario(funcionario, dia, dia, false);
        }

        public string HorasBatidasPorDiaPorFuncionario(Funcionario funcionario, DateTime dia)
        {

            string hora = string.Empty;
            string separador = " - ";
            var marcacoesDoDia = PointRepository.
                                    Listar().
                                    ToList().
                                    Where(p => p.DataValida.Date == dia.Date).
                                    Where(p => p.Funcionario.Id == funcionario.Id).
                                    Where(p => p.Contabilizar == true).
                                    OrderBy(p => p.DataValida).
                                    ToList();


            foreach (var ciclo in marcacoesDoDia)
            {

                if (hora.Equals(string.Empty))
                {
                    hora = ciclo.DataValida.ToString(FormatoHora);
                }
                else
                {
                    hora = hora + separador + ciclo.DataValida.ToString(FormatoHora);
                }
            }
            return hora;
        }
    }
}

