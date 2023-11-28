using conocelos_v3.Models;

namespace conocelos_v3.BD
{
    public partial class Candidatura
    {
        public int CandidaturaId { get; set; }

        public int TipoCandidaturaId { get; set; }

        public string NombreCandidatura { get; set; }

        public string Logo { get; set; }

        public bool Estatus { get; set; }

        public string acronimo { get; set; }

        // public virtual ICollection<Candidato> Candidatos { get; set; } = new List<Candidato>();

        public virtual TipoCandidatura TipoCandidatura { get; set; }
    }
}
