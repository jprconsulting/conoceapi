using System;
using System.Collections.Generic;

namespace conocelos_v3.Models;

public partial class Candidatura
{
    public int CandidaturaId { get; set; }

    public int TipoCandidaturaId { get; set; }

    public string NombreCandidatura { get; set; }

    public string Logo { get; set; }

    public string? Foto { get; set; }

    public bool Estatus { get; set; }

    public string Acronimo { get; set; }

    // public virtual ICollection<Candidato> Candidatos { get; set; } = new List<Candidato>();

    public virtual TipoCandidatura TipoCandidatura { get; set; }
}
