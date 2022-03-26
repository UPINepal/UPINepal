using Client.Interfaces;
using Server.Models;
using Shared.Interfaces;
using Shared.Models;

namespace Client.Implementations;

public class DataService:IDataService
{
    private readonly IRefitDataService _refitDataService;
    public DataService(IRefitDataService refitDataService)
    {
        _refitDataService = refitDataService;

    }
    public async Task<List<BankResponse>> GetBanks()
    {
      return await _refitDataService.GetBanks();
    }

    public async Task<List<AppResponse>> GetApps()
    {
        return await _refitDataService.GetApps();
    }
}
