using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedwork.Repository
{
    public class RepositoryBase<TEntidade> : IRepository<TEntidade> where TEntidade : EntityBase 
    {
        private DbContext Contexto { get; set; }

        public RepositoryBase(DbContext contexto)
        {
            Contexto = contexto;
        }

        public IQueryable<TEntidade> Listar()
        {
            return Contexto.Set<TEntidade>().AsQueryable();
        }

        public TEntidade PesquisarPeloId(Guid id){
            return Listar().SingleOrDefault(p => p.Id == id);
        }

        public void Remover(TEntidade entidade)
        {
            try
            {
                Contexto.Set<TEntidade>().Remove(entidade);
            }
            catch (Exception)
            {
                ///Log se for necessario
                throw;
            }
            
            
        }

        public void Salvar(TEntidade entidade)
        {
            var Set = Contexto.Set<TEntidade>();
            try
            {
                if (Set.Local.Any(e => e == entidade))
                    Contexto.Entry<TEntidade>(entidade).State = System.Data.Entity.EntityState.Modified;
                else
                    Contexto.Set<TEntidade>().Add(entidade);

            }
            catch(Exception ex)
            { 
                //Log se for necessario
                throw;
            }
          
        }

        public void Executar()
        {
            Contexto.SaveChanges();
        }
    }
}
