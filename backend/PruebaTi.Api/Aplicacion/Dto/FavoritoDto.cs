namespace PruebaTi.Api.Aplicacion.Dto
{
    public class FavoritoDto
    {
        public int Id { get; set; }
        public string IdExterno { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string? Autores { get; set; }
        public int? AnioPrimeraPublicacion { get; set; }
        public string? UrlPortada { get; set; }
    }
}
