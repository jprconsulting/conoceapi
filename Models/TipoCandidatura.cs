using System;
using System.Collections.Generic;

namespace conocelos_v3.Models;

public partial class TipoCandidatura
{
    public int TipoCandidaturaId { get; set; }

    public string NombreTipoCandidatura { get; set; }

   // public virtual ICollection<Candidatura> Candidaturas { get; set; } = new List<Candidatura>();
}
