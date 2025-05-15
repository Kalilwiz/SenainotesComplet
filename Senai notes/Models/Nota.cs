using System;
using System.Collections.Generic;

namespace Senai_notes.Models;

public partial class Nota
{
    public int NotaId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Texto { get; set; } = null!;

    public DateTime DataCriacao { get; set; }

    public DateTime DataAlteracao { get; set; }

    public bool Arquivado { get; set; }

    public string? Imagem { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<TagNota> TagNota { get; set; } = new List<TagNota>();

    public virtual Usuario? User { get; set; }
}
