namespace WebTruyenChu_Backend.Helper;

public interface IEmailSender
{
    Task SendEmailAsync(Message message);
}