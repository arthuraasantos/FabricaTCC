using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedwork.Conversores
{
    public abstract class ConversorBase<TOrigem, TDestino> : IConversor<TOrigem, TDestino> where TOrigem : class where TDestino : class
    {
        public abstract void AplicarValores(TDestino origem, TOrigem destino);

        public abstract void AplicarValores(TOrigem origem, TDestino destino);
       
        public TOrigem Converter(TDestino origem)
        {
            var destino = Activator.CreateInstance<TOrigem>();
            AplicarValores(origem, destino);
            return destino;
        }

        public TDestino Converter(TOrigem origem)
        {
            var destino = Activator.CreateInstance<TDestino>();
            AplicarValores(origem, destino);
            return destino;
        }
    }
}
