using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedwork.Conversores
{
    public interface IConversor<TOrigem, TDestino> where TOrigem : class where TDestino : class
    {
        void AplicarValores(TOrigem origem, TDestino destino);
        void AplicarValores(TDestino destino, TOrigem origem);

        TDestino Converter(TOrigem origem);
        TOrigem Converter(TDestino origem);


    }
}
