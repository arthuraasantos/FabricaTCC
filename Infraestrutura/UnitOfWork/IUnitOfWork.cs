using Dominio.Repository;

namespace Infraestrutura.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
