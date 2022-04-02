namespace Server.Models
{
    public class BankResponse
    {
        public BankResponse()
        {
            Tags = new List<string>();
        }
        public string Name { get; set; }
        public string PageLink { get; set; }
        public string LogoLink { get; set; }
        public List<string> Tags { get; set; }
    }
}
