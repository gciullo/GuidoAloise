namespace GuidoAloise.Api.Models
{
    public class Opera
    {
        public string Titolo { get; set; } = string.Empty;
        public string TitoloEng
        {
            get
            {
                if (string.IsNullOrEmpty(_titoloEng)) return Titolo;
                else return _titoloEng;
            }
            set
            {
                _titoloEng = value;
            }
        }
        private string? _titoloEng;
        public string Categoria
        {
            get
            {
                if (string.IsNullOrEmpty(_categoria)) return $"Anni '{(Anno - 1900) / 10 * 10}";
                else return _categoria;
            }
            set
            {
                _categoria = value;
            }
        }
        public string CategoriaEng
        {
            get
            {
                if (string.IsNullOrEmpty(_categoriaEng)) return $"19{(Anno - 1900) / 10 * 10}s";
                else return _categoriaEng;
            }
            set
            {
                _categoriaEng = value;
            }
        }
        private string? _categoria;
        private string? _categoriaEng;
        public string UrlImmagine { get; set; } = string.Empty;
        public int Anno { get; set; }
        public string Tecnica { get; set; } = string.Empty;
        public string TecnicaEng
        {
            get
            {
                if (string.IsNullOrEmpty(_tecnicaEng)) return Tecnica;
                else return _tecnicaEng;
            }
            set
            {
                _tecnicaEng = value;
            }
        }
        private string? _tecnicaEng;
        public string Dimensioni { get; set; } = string.Empty;
        public string? Descrizione { get; set; }
        public string? DescrizioneEng
        {
            get
            {
                if (string.IsNullOrEmpty(_descrizioneEng)) return Descrizione;
                else return _descrizioneEng;
            }
            set
            {
                _descrizioneEng = value;
            }
        }
        private string? _descrizioneEng;
    }
}
