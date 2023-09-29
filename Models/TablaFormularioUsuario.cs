using System;
using System.Collections.Generic;

namespace conocelos_v3.Models;

public partial class TablaFormularioUsuario
{
    public int FormularioUsuarioId { get; set; }

    public int? FormularioId { get; set; }

    public int? UsuarioId { get; set; }

    public virtual TablaFormulario FormularioUsuario { get; set; }

    public virtual Usuario Usuario { get; set; }
}
