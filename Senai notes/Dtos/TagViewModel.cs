namespace Senai_notes.Dtos
{
    public class TagViewModel
    {
        public int TagId { get; set; }

        public string Nome { get; set; } = null!;

        public int? UserId { get; set; }
    }
}
