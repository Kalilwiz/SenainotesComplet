namespace Senai_notes.Dtos
{
    public class Usuarioviewmodel
    {
        public int UserId { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime? DataCriacao { get; set; }
    }
}
