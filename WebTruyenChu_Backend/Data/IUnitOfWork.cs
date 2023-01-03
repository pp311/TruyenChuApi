namespace WebTruyenChu_Backend.Data;

public interface IUnitOfWork : IDisposable
{
    Task Commit();
}