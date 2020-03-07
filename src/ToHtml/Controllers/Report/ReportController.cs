using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq;
using ToHtml.Extensions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ToHtml.Controllers.Report {

    [Route("[controller]/[action]")]
    public class ReportController : Controller {

        readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public async Task<string> Html() {
            var html = await this.RenderViewAsync("Index", new { }, false);
            return html;
        }
    }
}

