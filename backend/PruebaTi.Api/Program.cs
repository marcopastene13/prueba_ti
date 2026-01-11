using Microsoft.EntityFrameworkCore;
using PruebaTi.Api.Infraestructura.Contexto;
using PruebaTi.Api.Aplicacion.Servicios;
using PruebaTi.Api.Infraestructura.ClientesExternos;

var builder = WebApplication.CreateBuilder(args);

// DbContext
var cadenaConexion = builder.Configuration.GetConnectionString("BaseDeDatos");
builder.Services.AddDbContext<AplicacionDbContext>(opciones =>
    opciones.UseSqlServer(cadenaConexion));

// Servicios
builder.Services.AddScoped<IServicioLibros, ServicioLibros>();
builder.Services.AddScoped<IServicioFavoritos, ServicioFavoritos>();
builder.Services.AddHttpClient<ILibrosApiCliente, OpenLibraryApiCliente>();

// CORS
const string PoliticaCors = "PermitirFrontend";

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy(PoliticaCors, politica =>
    {
        politica
            .WithOrigins(
                "https://congenial-space-spoon-r4w4w79v57v72wprv-4200.app.github.dev"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Controllers + Swagger
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

// aplicar CORS
app.UseCors(PoliticaCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
