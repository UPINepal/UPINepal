using Server.Models;
using Shared;
using Shared.Interfaces;
using Shared.Models;

namespace Server.Implementations
{
    public class DataService : IDataService
    {
        private readonly HttpClient _httpClient;
        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BankResponse>> GetBanks()
        {
            var banks = await _httpClient.GetFromJsonAsync<List<BankResponse>>(StaticContent.StaticUrl + "/json/banks.json");
            return banks;
        }

        public async Task<List<AppResponse>> GetApps()
        {
            var apps = await _httpClient.GetFromJsonAsync<List<AppResponse>>(StaticContent.StaticUrl + "/json/apps.json");
            return apps;
        }
    }
}
