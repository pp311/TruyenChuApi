namespace WebTruyenChu_Backend.Extensions;

public static class ServiceExtension
{
    public static void ConfigureCors(this IServiceCollection services)
{
      services.AddCors(options =>
      {
          options.AddPolicy("CorsPolicy",
              builder => builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
      });
}
}