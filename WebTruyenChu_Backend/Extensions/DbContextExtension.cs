using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebTruyenChu_Backend.Data;

namespace WebTruyenChu_Backend.Extensions;

public static class DbContextExtension
{
    public static void Update<TEntity>(this WebTruyenChuContext dbContext, TEntity entity) where TEntity : class
    {
        dbContext.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;
    }
}