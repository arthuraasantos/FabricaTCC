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

        public TEntidade PesquisarPeloId(Guid? id){
            if(id.HasValue)
                return Listar().SingleOrDefault(p => p.Id == (Guid)id);
            else            
                return null;            
        }

        public void Remover(TEntidade entidade)
        {
            try
            {
                Contexto.Entry(entidade).Entity.Exclusao = DateTime.Now;
                Contexto.Entry(entidade).State = EntityState.Modified;
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
                {
                    entidade.Alteracao = DateTime.Now;
                    Contexto.Entry(entidade).State = EntityState.Modified;
                }
                else
                {
                    entidade.Criacao = DateTime.Now;
                    entidade.Alteracao = entidade.Criacao;
                    Contexto.Set<TEntidade>().Add(entidade);
                }
            }
            catch(Exception)
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
