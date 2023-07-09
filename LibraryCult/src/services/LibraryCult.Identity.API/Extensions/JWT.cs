namespace LibraryCult.Identity.API.Extensions
{
    public class JWT
    {
        public string Secret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpirationTime { get; set; }
    }
}
