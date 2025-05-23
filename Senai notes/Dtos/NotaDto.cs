namespace Senai_notes.Dtos
{
    public class NotaDto
    {
        public string Titulo { get; set; } = null!;

        public string Texto { get; set; } = null!;

        public string? Imagem { get; set; }

        public int UserId { get; set; }

        public List<string> Tags { get; set; }
    }
}
