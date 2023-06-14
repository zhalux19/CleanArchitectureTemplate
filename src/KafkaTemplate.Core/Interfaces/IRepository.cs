using System.Linq.Expressions;

public interface IRepository<T>
{
    Task<List<T>> GetAll();
    Task<T> Create(T entity);
    Task<T> FindFirstByPredicate(Expression<Func<T, bool>> predicate);
    //Task<bool> ReplaceByPredicate(Expression<Func<T, bool>> predicate, T entity);
    //Task<List<T>> FindWhere(Expression<Func<T, bool>> predicate);
    //Task<T> FindSingleOrDefault(Expression<Func<T, bool>> predicate);
}