using Moq;
using PruebaTi.Api.Aplicacion.Servicios;
using PruebaTi.Api.Aplicacion.Dto;
using PruebaTi.Api.Infraestructura.ClientesExternos;
using Xunit;

namespace PruebaTi.Api.Tests.Servicios
{
    public class ServicioLibrosTests
    {
        [Fact]
        public async Task BuscarAsync_DebeRetornarListaDesdeCliente()
        {
            // Arrange
            var listaEsperada = new List<LibroDto>
            {
                new LibroDto
                {
                    IdExterno = "/libros/1",
                    Titulo = "Libro de prueba",
                    Autores = new List<string> { "Autor 1" },
                    AnioPrimeraPublicacion = 2000,
                    UrlPortada = "http://ejemplo.com/portada.jpg"
                }
            };

            var mockCliente = new Mock<ILibrosApiCliente>();
            mockCliente
                .Setup(c => c.BuscarLibrosPorTextoAsync("prueba", default))
                .ReturnsAsync(listaEsperada);

            var servicio = new ServicioLibros(mockCliente.Object);

            // Act
            var resultado = await servicio.BuscarAsync("prueba");

            // Assert
            Assert.Single(resultado);
            Assert.Equal("Libro de prueba", resultado[0].Titulo);
            Assert.Equal("Autor 1", resultado[0].Autores[0]);
        }
    }
}

