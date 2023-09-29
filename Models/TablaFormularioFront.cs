namespace conocelos_v3.Models
{
    public class TablaFormularioFront
    {
        public string FormularioIdFront { get; set; }

        public string FormNameFront { get; set; } = null!;

        public string GoogleFormIdFront { get; set; }

        public string GoogleEditFormIdFront { get; set; }

        public string SpreadsheetIdFront { get; set; }

        public string SheetNameFront { get; set; } = null!;

        public string ProjectIdFront { get; set; }

        public IFormFile ArchvioJson { get; set; }
    }
}
