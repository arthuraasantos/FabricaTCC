using Dominio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Services
{
    public interface IPontoEletronicoService
    {        
        void EfetuarMarcacaoDePonto(Funcionario funcionario);

        TimeSpan QuantidadeDeHorasTrabalhadasPorFuncionario(Funcionario funcionario, DateTime diaInicio, DateTime diaFinal);

        string HorasBatidasPorDiaPorFuncionario(Funcionario funcionario, DateTime dia);
         
    }
}
