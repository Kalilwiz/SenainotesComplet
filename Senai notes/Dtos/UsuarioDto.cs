namespace Senai_notes.Dtos
{
    public class UsuarioDto
    {
        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Senha { get; set; } = null!;

        public DateTime? DataCriacao { get; set; }

    }
}
