using System;
using System.Collections.Generic;

namespace Senai_notes.Models;

public partial class AuditoriaGeral
{
    public int AuditoriaId { get; set; }

    public string NomeTabela { get; set; } = null!;

    public string TipoAcao { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public string? DadosAntigos { get; set; }

    public string? DadosNovos { get; set; }

    public DateTime DataAcao { get; set; }
}
