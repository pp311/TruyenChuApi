using WebTruyenChu_Backend.Services;
using WebTruyenChu_Backend.Services.Interfaces;

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

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IGenreService, GenreService>();
    }
}