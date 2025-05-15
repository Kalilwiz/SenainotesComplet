using System;
using System.Collections.Generic;

namespace Senai_notes.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<TagNota> TagNota { get; set; } = new List<TagNota>();
}
