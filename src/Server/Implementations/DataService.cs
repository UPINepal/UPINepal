using Server.Models;
using Shared;
using Shared.Interfaces;
using Shared.Models;

namespace Server.Implementations
{
    public class DataService : IDataService
    {
        private readonly List<BankResponse> _banks;
        private readonly List<AppResponse> _apps;

        public DataService()
        {
            var httpClient = new HttpClient();
            _apps = httpClient.GetFromJsonAsync<List<AppResponse>>(StaticContent.StaticUrl + "/json/apps.json").Result;
            _banks = httpClient.GetFromJsonAsync<List<BankResponse>>(StaticContent.StaticUrl + "/json/banks.json")
                .Result;
        }

        public async Task<List<BankResponse>> GetBanks()
        {
            return _banks;
        }

        public async Task<List<AppResponse>> GetApps()
        {
            return _apps;
        }
    }
}
