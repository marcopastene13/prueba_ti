namespace PruebaTi.Api.Aplicacion.Dto
{
    public class LibroDto
    {
        public string IdExterno { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public List<string> Autores { get; set; } = new();
        public int? AnioPrimeraPublicacion { get; set; }
        public string? UrlPortada { get; set; }
    }
}
