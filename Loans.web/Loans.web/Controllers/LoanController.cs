using Loans.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Loans.web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILogger<LoanController> _logger;

        public LoanController(ILogger<LoanController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Loan> Get()
        {
            return null;
        }
    }
}