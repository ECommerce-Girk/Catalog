using System.Text;
using Base;
using Catalog.Core.Products;
using Catalog.Infra.Database;
using Catalog.Infra.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddOpenApi();
services.AddProblemDetails();
//services.AddExceptionHandler<CustomExceptionHandler>();
services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!))
        };
    });


services.AddDbContext<CatalogDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("Db")));
services.AddScoped<IUnitOfWork, CatalogDbContext>();

services.AddScoped<CreateProduct>();
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.UseExceptionHandler();
app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();