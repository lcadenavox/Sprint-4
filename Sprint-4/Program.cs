using Microsoft.EntityFrameworkCore;
using Sprint_4.Data;
using Sprint_4.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "API Oficina", Version = "v1", Description = "API para gerenciamento de motos, mecânicos e oficinas." });
    options.EnableAnnotations();
});
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("OficinaDb"));
builder.Services.AddScoped<MotoService>();
builder.Services.AddScoped<MecanicoService>();
builder.Services.AddScoped<OficinaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
