using Microsoft.AspNetCore.Mvc;
using QueryConverter.Core.Convension.Processor;
using QueryConverter.Shared.Dto;
using QueryConverter.UI.Models;
using System.Diagnostics;

namespace QueryConverter.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;

        public HomeController(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public IActionResult Index()
        {
            return View(new ConvertModel());
        }

        [HttpPost]
        public IActionResult Index(ConvertModel model)
        {

            var result = _queryProcessor.ProcessSqlQuery(model.SQLQuery);

            return View(result);
        }

        public IActionResult About() => View();
        public IActionResult Privacy() => View();


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
