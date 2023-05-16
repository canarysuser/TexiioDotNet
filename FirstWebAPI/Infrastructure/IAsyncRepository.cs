namespace FirstWebAPI.Infrastructure
{
    public interface IAsyncRepository<TEntity, TIdentity>
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);

        Task RemoveAsync(TIdentity id);
        Task UpsertAsync(TEntity item);

    }
}
