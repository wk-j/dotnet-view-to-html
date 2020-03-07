using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq;
using ToHtml.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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


namespace ToHtml.Extensions {
    public static class ViewResultExtensions {
        public static async Task<string> RenderViewAsync<TModel>(this Controller controller, string viewName, TModel model, bool partial = false) {
            if (string.IsNullOrEmpty(viewName)) {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using (var writer = new StringWriter()) {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, !partial);

                if (viewResult.Success == false) {
                    return $"A view with the name {viewName} could not be found";
                }

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}