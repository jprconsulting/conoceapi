using System;
using System.Collections.Generic;

namespace conocelos_v3.Models;

public partial class Genero
{
    public int GeneroId { get; set; }

    public string NombreGenero { get; set; }

    // public virtual ICollection<Candidato> Candidatos { get; set; } = new List<Candidato>();
}
