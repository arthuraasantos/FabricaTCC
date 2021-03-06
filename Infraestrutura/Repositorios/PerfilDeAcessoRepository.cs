﻿
using Dominio.Model;
using Dominio.Repository;
using Seedwork.Repository;

namespace Infraestrutura.Repositorios
{
    public class PerfilDeAcessoRepository : RepositoryBase<PerfilDeAcesso>, IPerfilDeAcessoRepository
    {
        private readonly PontoContext Context;

        public PerfilDeAcessoRepository(PontoContext context) : base(context)
        {
            Context = context;
        }
    }
}
