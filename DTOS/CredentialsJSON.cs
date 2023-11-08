namespace conocelos_v3.DTOS
{
    public class CredentialsJSON
    {
        public string type { get; set; } = null!;

        public string project_id { get; set; } = null!;

        public string private_key_id { get; set; } = null!;

        public string private_key { get; set; } = null!;

        public string client_email { get; set; } = null!;

        public string client_id { get; set; } = null!;

        public string auth_uri { get; set; } = null!;

        public string token_uri { get; set; } = null!;

        public string auth_provider_x509_cert_url { get; set; } = null!;

        public string client_x509_cert_url { get; set; } = null!;

        public string universe_domain { get; set; } = null!;
    }
}
