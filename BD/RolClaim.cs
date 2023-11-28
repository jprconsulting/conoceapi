namespace conocelos_v3.BD
{
    public partial class RolClaim
    {
        public int RolClaimId { get; set; }

        public int RolId { get; set; }

        public string ClaimType { get; set; } = null!;

        public ulong ClaimValue { get; set; }
    }

}
