using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace WebTruyenChu_Backend.Data;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity?> GetSingleById(int id);
    IQueryable<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetAll();
    public IQueryable<TEntity> GetAllQueryable();
    public IQueryable<TEntity> GetAllQueryable(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);
}