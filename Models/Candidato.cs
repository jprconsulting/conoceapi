using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace conocelos_v3.Models;

public partial class Candidato
{
    public int? CandidatoId { get; set; }

    public string Nombre { get; set; }

    public string ApellidoPaterno { get; set; }

    public string ApellidoMaterno { get; set; }

    public string? SobrenombrePropietario { get; set; }
    //3

    public string NombreSuplente { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    public string? DireccionCasaCampania { get; set; }

    public string? TelefonoPublico { get; set; }
    //7

    public string? Email { get; set; }

    public string? PaginaWeb { get; set; }

    public string? Facebook { get; set; }

    public string? Twitter { get; set; }
    // 11

    public string? Instagram { get; set; }

    public string? Tiktok { get; set; }

    public string? Foto { get; set; }

    public bool Estatus { get; set; }
    //15
    public int GeneroId { get; set; }

    public int CargoId { get; set; }

    public int EstadoId { get; set; }

    public int DistritoLocalId { get; set; }

    public int AyuntamientoId { get; set; }

    public int ComunidadId { get; set; }

    public int CandidaturaId { get; set; }
    // 19

    public virtual Candidatura Candidatura { get; set; }

    public virtual Cargo Cargo { get; set; }

    public virtual Estado Estado { get; set; }

    public virtual Genero Genero { get; set; }

    public virtual DistritoLocal DistritoLocal { get; set; }

    public virtual Ayuntamiento Ayuntamiento { get; set; }

    public virtual Comunidad Comunidad { get; set; }

}