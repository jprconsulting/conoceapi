using System;
using System.Collections.Generic;

namespace conocelos_v3.Models;

public partial class PreguntaCuestionarioGoogleForm
{
    public int PreguntaCuestionarioId { get; set; }

    public int FormularioId { get; set; }

    public string Pregunta { get; set; } = null!;
}
