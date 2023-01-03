using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace WebTruyenChu_Backend.Data;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    private WebTruyenChuContext _dbContext;
    private readonly DbSet<TEntity> dbSet;

    public RepositoryBase(WebTruyenChuContext dbContext)
    {
        _dbContext = dbContext;
        dbSet = _dbContext.Set<TEntity>();
    }

    public virtual void Add(TEntity entity)
    {
        dbSet.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(TEntity entity)
    {
         dbSet.Remove(entity);
    }

    public virtual async Task<TEntity?> GetSingleById(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual IQueryable<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> predicate)
    {
        return dbSet.Where(predicate);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        return await dbSet.ToListAsync();
    }

    public virtual IQueryable<TEntity> GetAllQueryable()
    {
        return dbSet.AsNoTracking();
    }

    public virtual IQueryable<TEntity> GetAllQueryable(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include)
    {
        var query = dbSet.AsQueryable();
        if (include is not null)
        {
            query = include(query);
        }
        return query.AsNoTracking();
    }
}
