namespace Senai_notes.Dtos
{
    public class NotaDto
    {
        public string Titulo { get; set; } = null!;

        public string Texto { get; set; } = null!;

        public DateTime DataCriacao { get; set; }

        public DateTime DataAlteracao { get; set; }

        public bool Arquivado { get; set; }

        public string? Imagem { get; set; }
    }
}
