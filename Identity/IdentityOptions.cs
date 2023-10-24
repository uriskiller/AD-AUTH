namespace AD_Auth.Identity
{
    public class IdentityOptions
    {
        public string? Hostname { get; set; }
        public int Port { get; set; }
        public string? BaseSearch { get; set; }
        public string? SearchPattern { get; set; }
        public string? SearchEmployeePattern { get; set; }
        public string? ModulesContainer { get; set; }
        public string? AdminAccount { get; set; }
        public string? SecretKey { get; set; }
    }
}
