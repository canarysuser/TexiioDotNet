namespace FirstWebApp.Infrastructure
{
    public interface IRepository<TEntity, TIdentity>
    {
        List<TEntity> GetAll();
        TEntity Get(TIdentity id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TIdentity id);
    }
}
