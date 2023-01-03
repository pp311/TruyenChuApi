namespace WebTruyenChu_Backend.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly WebTruyenChuContext _dbContext;
    private bool _disposed;
    
    public UnitOfWork(WebTruyenChuContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task Commit()
    {
        throw new NotImplementedException();
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}