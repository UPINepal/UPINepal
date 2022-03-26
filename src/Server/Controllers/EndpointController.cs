using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Shared.Interfaces;
using Shared.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EndpointController : ControllerBase
    {
        private readonly ILogger<EndpointController> _logger;
        private readonly IDataService _dataService;

        public EndpointController(ILogger<EndpointController> logger,
            IDataService dataService
        )
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpGet("apps")]
        public async Task<IEnumerable<AppResponse>> GetApps()
        {
            return await _dataService.GetApps();
        }

        [HttpGet("banks")]
        public async Task<IEnumerable<BankResponse>> GetBanks()
        {
            return await _dataService.GetBanks();
        }
    }
}
