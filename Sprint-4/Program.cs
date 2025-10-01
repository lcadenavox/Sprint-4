using Microsoft.EntityFrameworkCore;
using Sprint_4.Data;
using Sprint_4.Services;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "API Depósito", Version = "v1", Description = "API para gerenciamento de motos, mecânicos e depósitos." });
    options.EnableAnnotations();
    options.ExampleFilters();

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("DepositoDb"));
builder.Services.AddScoped<MotoService>();
builder.Services.AddScoped<MecanicoService>();
builder.Services.AddScoped<DepositoService>();

// CORS
const string AllowExpo = "AllowExpo";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowExpo, policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:8081",
                "http://127.0.0.1:8081",
                "http://localhost:19006"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Ative a policy de CORS antes de Authorization/MapControllers
app.UseCors(AllowExpo);

app.UseAuthorization();

app.MapControllers();
// app.MapControllers().RequireCors(AllowExpo); // opcional

app.Run();