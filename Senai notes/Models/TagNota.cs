using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Senai_notes.Models;

public partial class TagNota
{
    public int TagNotaId { get; set; }

    public int? NotaId { get; set; }

    public int? TagId { get; set; }

    [JsonIgnore]
    public virtual Nota? Nota { get; set; }

    public virtual Tag? Tag { get; set; }
}
