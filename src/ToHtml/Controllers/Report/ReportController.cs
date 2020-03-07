using Microsoft.AspNetCore.Mvc;

namespace ToHtml.Controllers.Report {

    [Route("[controller]/[action]")]
    public class ReportController : Controller {

        [HttpGet]
        public IActionResult Index() {
            return View();
        }
    }
}