﻿using Dominio;
using Dominio.Model;
using Dominio.Repository;
using Seedwork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorios
{
    public class SolicitacaoRepository : RepositoryBase<Solicitacao>, ISolicitacaoRepository
    {
        public SolicitacaoRepository(MyContext context)
            : base(context)
        {

        }
    }
}