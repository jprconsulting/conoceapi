using System;
using System.Collections.Generic;

namespace conocelos_v3.Models;

public partial class UsuarioAutenticado
{
    public int UsuarioId { get; set; }

    public string NombreUsuario { get; set; }

    public string Clave { get; set; }
}
