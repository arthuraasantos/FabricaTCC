using Dominio.Model;
using Dominio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seedwork;
using Seedwork.Const;

namespace Dominio.Services
{
    public class PontoEletronicoService : IPontoEletronicoService
    {
        private string FormatoHora = "HH:mm";
        private IPontoRepository PontoRepository { get; set; }
        public PontoEletronicoService(IPontoRepository pontoRepository)
        {
            PontoRepository = pontoRepository;
        }
        public void EfetuarMarcacaoDePonto(Funcionario funcionario)
        {
            var novoPonto = new Ponto()
           {
               Id = Guid.NewGuid(),
               DataDaMarcacao = DateTime.Now,
               DataValida = DateTime.Now,
               DataAjuste = null,
               MotivoAjuste = String.Empty,
               AjusteAprovado = (int)EnumPonto.Aprovacao.Nada,
               Funcionario = funcionario
           };

            PontoRepository.Salvar(novoPonto);
            PontoRepository.Executar();
        }

        public TimeSpan QuantidadeDeHorasTrabalhadasPorFuncionario(Funcionario funcionario, DateTime diaInicio, DateTime diaFinal)
        {
            var marcacoesDoDia = PontoRepository.
                                    Listar().
                                    ToList().
                                    Where(p => p.DataDaMarcacao.Date >= diaInicio.Date).
                                    Where(p => p.DataDaMarcacao.Date <= diaFinal.Date).
                                    OrderBy(p => p.DataDaMarcacao).ToList();

            if (marcacoesDoDia.Count % 2 != 0)
                return new TimeSpan();

            var totalDeCiclos = marcacoesDoDia.Count / 2;

            var horasTrabalhadas = new TimeSpan();

            for (int i = 0; i < totalDeCiclos; i = i + 2)
            {
                horasTrabalhadas = horasTrabalhadas.Add(marcacoesDoDia[i + 1].DataDaMarcacao - marcacoesDoDia[i].DataDaMarcacao);
            }

            return horasTrabalhadas;
        }

        public string HorasBatidasPorDiaPorFuncionario(Funcionario funcionario, DateTime dia)
        {

            string hora = string.Empty;
            string separador = " - ";
            var marcacoesDoDia = PontoRepository.
                                    Listar().
                                    ToList().
                                    Where(p => p.DataDaMarcacao.Date == dia.Date).
                                    OrderBy(p => p.DataDaMarcacao).ToList();


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
