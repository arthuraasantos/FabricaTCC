
namespace Dominio.Interface
{
    public interface IEntityBase<TEntity>
        where TEntity : class
    {
        bool IsValid(TEntity entity);
    }
}
