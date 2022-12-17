namespace Shared.Models
{
    public class AppResponse
    {
        public AppResponse()
        {
            Tags = new List<string>();
        }
        public string Name { get; set; }
        public string PageLink { get; set; }
        public string LogoLink { get; set; }
        public string AppStoreLink { get; set; }
        public string PlayStoreLink { get; set; }
        public List<string> Tags { get; set; }
    }
} 
 