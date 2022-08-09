using Loans.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Loans.web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessClientController : ControllerBase
    {
        private readonly ILogger<LoanController> _logger;

        public BusinessClientController(ILogger<LoanController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<BusinessClient> Get()
        {
            return null;
        }
    }
}