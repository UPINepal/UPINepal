using Refit;
using Server.Models;
using Shared.Models;

namespace Client.Interfaces;

public interface IRefitDataService
{
    [Get("/Endpoint/banks")]
    Task<List<BankResponse>> GetBanks();

    [Get("/Endpoint/apps")]
    Task<List<AppResponse>> GetApps();
}
