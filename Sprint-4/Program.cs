using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Sprint_4.Data;
using Sprint_4.Services;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("x-api-version"),
        new UrlSegmentApiVersionReader()
    );
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "VVV"; // e.g. v1, v2
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();

// Swagger + examples + versioning + API Key security
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.ExampleFilters();

    var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(description.GroupName, new OpenApiInfo
        {
            Title = $"API Depósito ({description.GroupName})",
            Version = description.ApiVersion.ToString(),
            Description = "API para gerenciamento de motos, mecânicos, depósitos e oficinas."
        });
    }

    // API Key security definition
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Chave de API via header X-Api-Key",
        Name = "X-Api-Key",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" } },
            Array.Empty<string>()
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (System.IO.File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

// DB e Services
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("DepositoDb"));
builder.Services.AddScoped<MotoService>();
builder.Services.AddScoped<MecanicoService>();
builder.Services.AddScoped<DepositoService>();
builder.Services.AddScoped<OficinaService>();
builder.Services.AddSingleton<SentimentService>();

// CORS (inclui http e https das portas usadas pelo Expo/Metro)
const string AllowExpo = "AllowExpo";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowExpo, policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:8081",
                "http://127.0.0.1:8081",
                "http://localhost:8082",
                "http://127.0.0.1:8082",
                "http://localhost:19006",
                "https://localhost:8081",
                "https://127.0.0.1:8081",
                "https://localhost:19006"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Health
builder.Services.AddHealthChecks();

var app = builder.Build();

// Swagger (Dev)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

// Aplique CORS ANTES do middleware de API Key
app.UseCors(AllowExpo);

// API Key middleware (permite OPTIONS e health/swagger sem chave)
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value?.ToLowerInvariant() ?? string.Empty;

    // Libera preflight para passar CORS
    if (HttpMethods.Options.Equals(context.Request.Method, StringComparison.OrdinalIgnoreCase))
    {
        await next();
        return;
    }

    // Libera health e swagger
    if (path.StartsWith("/swagger") || path.Contains("/health"))
    {
        await next();
        return;
    }

    if (!context.Request.Headers.TryGetValue("X-Api-Key", out var provided))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("API Key missing");
        return;
    }
    var configuredKey = app.Configuration["ApiKey"] ?? "dev-key"; // default local
    if (!string.Equals(provided.ToString(), configuredKey, StringComparison.Ordinal))
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsync("Invalid API Key");
        return;
    }

    await next();
});

app.UseAuthorization();

app.MapControllers();

// Health endpoints (mantém /health e cobre /api e /api/v1)
app.MapHealthChecks("/health");
app.MapHealthChecks("/api/health");
app.MapHealthChecks("/api/v1/health");

app.Run();