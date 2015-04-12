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
               DataMarcacao = DateTime.Now,
               DataValida = DateTime.Now,
               Funcionario = funcionario,
               FolhaPonto = null // TODO : Passar objeto folha de ponto
           };

            PontoRepository.Salvar(novoPonto);
            PontoRepository.Executar();
        }

        public TimeSpan QuantidadeDeHorasTrabalhadasPorFuncionario(Funcionario funcionario, DateTime diaInicio, DateTime diaFinal)
        {
            // TODO : Verificar esta função : QuantidadeDeHorasTrabalhadasPorFuncionario
            var marcacoesDoDia = PontoRepository.
                                    Listar().
                                    ToList().
                                    Where(p => p.DataValida.Date >= diaInicio.Date).
                                    Where(p => p.DataValida.Date <= diaFinal.Date).
                                    OrderBy(p => p.DataValida).
                                    ToList();

            if (marcacoesDoDia.Count % 2 != 0)
                return new TimeSpan();

            var totalDeCiclos = marcacoesDoDia.Count / 2;

            var horasTrabalhadas = new TimeSpan();

            for (int i = 0; i < totalDeCiclos; i = i + 2)
            {
                horasTrabalhadas = horasTrabalhadas.Add(marcacoesDoDia[i + 1].DataValida - marcacoesDoDia[i].DataValida);
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
                                    Where(p => p.DataValida.Date == dia.Date).
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
