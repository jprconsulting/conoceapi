using System;
using System.Collections.Generic;

namespace conocelos_v3.Models;

public partial class RutaImagen
{
    public int ImagenId { get; set; }

    public int? UsuarioId { get; set; }

    public string Descripcion { get; set; }

    public string Direccion { get; set; }

    public virtual Usuario Usuario { get; set; }
}
