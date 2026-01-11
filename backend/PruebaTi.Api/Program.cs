using Microsoft.EntityFrameworkCore;
using PruebaTi.Api.Infraestructura.Contexto;

var builder = WebApplication.CreateBuilder(args);

// Configuración de DbContext
var cadenaConexion = builder.Configuration.GetConnectionString("BaseDeDatos");
builder.Services.AddDbContext<AplicacionDbContext>(opciones =>
    opciones.UseSqlServer(cadenaConexion));

// Servicios MVC / controllers
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
