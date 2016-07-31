﻿using Dominio.Model;
using Dominio.Repository;
using Seedwork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorios
{
    public class HorarioDeExpedienteRepository : RepositoryBase<HorarioDeExpediente>, IHorarioDeExpedienteRepository
    {
        private readonly PontoContext Context;

        public HorarioDeExpedienteRepository(PontoContext context) : base(context)
        {
            Context = context;
        }
    }
}
