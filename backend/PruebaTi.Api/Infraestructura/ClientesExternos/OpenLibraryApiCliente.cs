using System.Net.Http.Json;
using PruebaTi.Api.Aplicacion.Dto;

namespace PruebaTi.Api.Infraestructura.ClientesExternos
{
    public class OpenLibraryApiCliente : ILibrosApiCliente
    {
        private readonly HttpClient _httpClient;

        public OpenLibraryApiCliente(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://openlibrary.org/");
        }

        public async Task<List<LibroDto>> BuscarLibrosPorTextoAsync(string textoBusqueda, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                return new List<LibroDto>();
            }

            var url = $"search.json?q={Uri.EscapeDataString(textoBusqueda)}";

            using var respuesta = await _httpClient.GetAsync(url, cancellationToken);

            if (!respuesta.IsSuccessStatusCode)
            {
                // Podrías loguear el error; por ahora devolvemos lista vacía
                return new List<LibroDto>();
            }

            var contenido = await respuesta.Content.ReadFromJsonAsync<OpenLibrarySearchResponse>(cancellationToken: cancellationToken);

            if (contenido?.Docs is null)
            {
                return new List<LibroDto>();
            }

            var libros = contenido.Docs.Select(doc => new LibroDto
            {
                IdExterno = doc.Key ?? string.Empty,
                Titulo = doc.Title ?? string.Empty,
                Autores = doc.AuthorName ?? new List<string>(),
                AnioPrimeraPublicacion = doc.FirstPublishYear,
                UrlPortada = doc.CoverI.HasValue
                    ? $"https://covers.openlibrary.org/b/id/{doc.CoverI}-M.jpg"
                    : null
            }).ToList();

            return libros;
        }

        private class OpenLibrarySearchResponse
        {
            public List<OpenLibraryDoc> Docs { get; set; } = new();
        }

        private class OpenLibraryDoc
        {
            public string? Key { get; set; }
            public string? Title { get; set; }
            public List<string>? AuthorName { get; set; }
            public int? FirstPublishYear { get; set; }
            public int? CoverI { get; set; }
        }
    }
}
