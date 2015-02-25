using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedwork.Repository
{
    public interface IRepository<TEntidade> where TEntidade : EntityBase
    {
        IQueryable<TEntidade> Listar();
        void Salvar(TEntidade entidade);
        void Remover(TEntidade entidade);
        TEntidade PesquisarPeloId(Guid chave);

        void Executar();

    }
}
