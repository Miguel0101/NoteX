namespace NoteX.Domain.Common.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(Ulid id);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}