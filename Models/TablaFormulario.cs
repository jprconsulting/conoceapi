using System;
using System.Collections.Generic;

namespace conocelos_v3.Models;

public partial class TablaFormulario
{
    public int FormularioId { get; set; }

    public string FormName { get; set; }

    public string GoogleFormId { get; set; }

    public string GoogleEditFormId { get; set; }

    public string SpreadsheetId { get; set; }

    public string SheetName { get; set; }

    public string ProjectId { get; set; }

    public string Type { get; set; }

    public string ProjectId1 { get; set; }

    public string PrivateKeyId { get; set; }

    public string PrivateKey { get; set; }

    public string ClientEmail { get; set; }

    public string ClientId { get; set; }

    public string AuthUri { get; set; }

    public string TokenUri { get; set; }

    public string AuthProviderX509CertUrl { get; set; }

    public string ClientX509CertUrl { get; set; }

    public string UniverseDomain { get; set; }

    // public virtual TablaFormularioUsuario TablaFormularioUsuario { get; set; }
}
