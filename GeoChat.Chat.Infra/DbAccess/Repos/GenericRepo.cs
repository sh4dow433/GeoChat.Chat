using GeoChat.Chat.Core.Repos;
using Microsoft.EntityFrameworkCore;

namespace GeoChat.Chat.Infra.DbAccess.Repos;

public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class, new()
{
    protected DbSet<TEntity> EntitySet { get; }

    public GenericRepo(AppDbContext dbContext)
    {
        EntitySet = dbContext.Set<TEntity>();
    }

    public TEntity Create(TEntity entity)
    {
        EntitySet.Add(entity);
        return entity;
    }

    public void Delete(TEntity entity) => EntitySet.Remove(entity);

    public async Task<IEnumerable<TEntity>> GetAllAsync() => await EntitySet.ToListAsync();

    public async Task<TEntity?> GetAsync(object id) => await EntitySet.FindAsync(id);

    public void Update(TEntity entity) => EntitySet.Update(entity);
}