namespace Data.Access.Interfaces
{

    using System.Linq.Expressions;

    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string includeProperties = "");
        Task<T?> GetByIdAsync(long id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<T?> FindFirstOrDefaultAsync(Expression<Func<T, bool>> predicate); 
    }




}
