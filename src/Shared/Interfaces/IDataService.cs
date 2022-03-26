using Server.Models;
using Shared.Models;

namespace Shared.Interfaces
{
    public interface IDataService
    {
        Task<List<BankResponse>> GetBanks();
        Task<List<AppResponse>> GetApps();
    }
}
