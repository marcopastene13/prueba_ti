using Microsoft.EntityFrameworkCore;
using PruebaTi.Api.Aplicacion.Servicios;
using PruebaTi.Api.Dominio.Entidades;
using PruebaTi.Api.Infraestructura.Contexto;
using Xunit;

namespace PruebaTi.Api.Tests.Servicios
{
    public class ServicioFavoritosListarTests
    {
        private AplicacionDbContext CrearContextoEnMemoria()
        {
            var opciones = new DbContextOptionsBuilder<AplicacionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var contexto = new AplicacionDbContext(opciones);

            contexto.Usuarios.Add(new Usuario
            {
                Id = 1,
                Nombre = "Usuario Pruebas"
            });

            contexto.Favoritos.Add(new Favorito
            {
                UsuarioId = 1,
                IdExterno = "/libros/1",
                Titulo = "Libro 1"
            });

            contexto.Favoritos.Add(new Favorito
            {
                UsuarioId = 1,
                IdExterno = "/libros/2",
                Titulo = "Libro 2"
            });

            contexto.SaveChanges();

            return contexto;
        }

        [Fact]
        public async Task ListarAsync_DebeRetornarFavoritosDelUsuario()
        {
            using var contexto = CrearContextoEnMemoria();
            var servicio = new ServicioFavoritos(contexto);

            var resultado = await servicio.ListarAsync(1);

            Assert.Equal(2, resultado.Count);
            Assert.Contains(resultado, f => f.Titulo == "Libro 1");
            Assert.Contains(resultado, f => f.Titulo == "Libro 2");
        }
    }
}
