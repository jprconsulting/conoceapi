using System;
using System.Collections.Generic;

namespace conocelos_v3.Models;

public partial class Cargo
{
    public int CargoId { get; set; }

    public string NombreCargo { get; set; }

    // public virtual ICollection<Candidato> Candidatos { get; set; } = new List<Candidato>();
}
