using Microsoft.EntityFrameworkCore;
using PruebaTi.Api.Aplicacion.Dto;
using PruebaTi.Api.Aplicacion.Servicios;
using PruebaTi.Api.Infraestructura.Contexto;
using Xunit;

namespace PruebaTi.Api.Tests.Servicios
{
    public class ServicioFavoritosTests
    {
        private AplicacionDbContext CrearContextoEnMemoria()
        {
            var opciones = new DbContextOptionsBuilder<AplicacionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var contexto = new AplicacionDbContext(opciones);

            // Semilla: usuario fijo con Id = 1
            contexto.Usuarios.Add(new PruebaTi.Api.Dominio.Entidades.Usuario
            {
                Id = 1,
                Nombre = "Usuario Pruebas"
            });

            contexto.SaveChanges();

            return contexto;
        }

        [Fact]
        public async Task AgregarAsync_DebeEvitarDuplicados()
        {
            // Arrange
            using var contexto = CrearContextoEnMemoria();
            var servicio = new ServicioFavoritos(contexto);

            var request = new CrearFavoritoRequest
            {
                IdExterno = "/libros/1",
                Titulo = "Libro de prueba"
            };

            // Act: agregamos una vez
            var resultado1 = await servicio.AgregarAsync(1, request);
            // Act: intentamos agregar el mismo otra vez
            var resultado2 = await servicio.AgregarAsync(1, request);

            // Assert
            Assert.True(resultado1.Creado);
            Assert.False(resultado2.Creado);
            Assert.Equal("El libro ya está agregado a favoritos.", resultado2.MensajeError);
        }

        [Fact]
        public async Task AgregarAsync_DebeValidarCamposObligatorios()
        {
            using var contexto = CrearContextoEnMemoria();
            var servicio = new ServicioFavoritos(contexto);

            var request = new CrearFavoritoRequest
            {
                IdExterno = "",
                Titulo = ""
            };

            var resultado = await servicio.AgregarAsync(1, request);

            Assert.False(resultado.Creado);
            Assert.Equal("IdExterno y Titulo son obligatorios.", resultado.MensajeError);
        }

        [Fact]
        public async Task EliminarAsync_FavoritoInexistente_DebeRetornarMensajeClaro()
        {
            using var contexto = CrearContextoEnMemoria();
            var servicio = new ServicioFavoritos(contexto);

            var resultado = await servicio.EliminarAsync(1, 999);

            Assert.False(resultado.Eliminado);
            Assert.Equal("No se encontró el favorito indicado.", resultado.MensajeError);
        }
    }
}
