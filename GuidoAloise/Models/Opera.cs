namespace GuidoAloise.Models
{
    public class Opera
    {
        public string Titolo { get; set; } = string.Empty;
        public string TitoloEng { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string CategoriaEng { get; set; } = string.Empty;
        public string UrlImmagine { get; set; } = string.Empty;
        public int Anno { get; set; }
        public string Tecnica { get; set; } = string.Empty;
        public string TecnicaEng { get; set; } = string.Empty;
        public string Dimensioni { get; set; } = string.Empty;
        public string? Descrizione { get; set; }
        public string? DescrizioneEng { get; set; }
    }
}
