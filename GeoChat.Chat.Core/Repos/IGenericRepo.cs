namespace GeoChat.Chat.Core.Repos;

public interface IGenericRepo<TEntity> where TEntity : class, new()
{
    Task<TEntity?> GetAsync(object id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    TEntity Create(TEntity entity);
    void Update (TEntity entity);
    void Delete (TEntity entity);   
}