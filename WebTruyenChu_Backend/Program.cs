using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebTruyenChu_Backend.Data;
using WebTruyenChu_Backend.Entities;
using WebTruyenChu_Backend.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.AddDbContext<WebTruyenChuContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebTruyenChuContext"));
});
builder.Services.AddIdentity<User, IdentityRole<int>>(opt =>
{
    opt.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<WebTruyenChuContext>().AddDefaultTokenProviders();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.All
    });
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();