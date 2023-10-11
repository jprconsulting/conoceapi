using System;
using System.Collections.Generic;

namespace conocelos_v3.Models;

public partial class GoogleFormUsuario
{
    public int FormularioUsuarioId { get; set; }

    public int FormularioId { get; set; }

    public int UsuarioId { get; set; }
}
