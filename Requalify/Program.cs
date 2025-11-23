using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Requalify.Data;
using Requalify.Middleware;
using Requalify.Services;
using Oracle.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors.Infrastructure;

// =========================
//      CONFIGURAÇÃO
// =========================

var builder = WebApplication.CreateBuilder(args);

// CONTROLLERS
builder.Services.AddControllers();

// API VERSIONING
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// HEALTH CHECKS
builder.Services.AddHealthChecks();

// SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Requalify API", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Requalify API", Version = "v2" });

    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Use o header: X-API-KEY",
        Type = SecuritySchemeType.ApiKey,
        Name = "X-API-KEY",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "ApiKey",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

// DATABASE - ORACLE
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// SERVICES
builder.Services.AddScoped<ServicoUsuarios>();
builder.Services.AddScoped<ServicoCursos>();
builder.Services.AddScoped<ServicoVagas>();
builder.Services.AddScoped<ServicoNoticias>();

var app = builder.Build();

// MIDDLEWARE
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");
    });
}

app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

// =============================================
//  ESSA CLASSE DEVE SEMPRE FICAR NO FINAL!!!
//  NECESSÁRIA PARA OS TESTES DE INTEGRAÇÃO
// =============================================
public partial class Program { }
