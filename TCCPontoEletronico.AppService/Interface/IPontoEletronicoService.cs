using Dominio.Model;
using System;

namespace TCCPontoEletronico.AppService.Interface
{
    public interface IPontoEletronicoService
    {        
        void EfetuarMarcacaoDePonto(Funcionario funcionario);

        TimeSpan QuantidadeDeHorasTrabalhadasPorFuncionario(Funcionario funcionario, DateTime diaInicio, DateTime diaFinal, Boolean descontarHoras = true);

        TimeSpan QuantidadeDeHorasTrabalhadasPorFuncionarioPorDia(Funcionario funcionario, DateTime dia);

        string HorasBatidasPorDiaPorFuncionario(Funcionario funcionario, DateTime dia);
    }
}
