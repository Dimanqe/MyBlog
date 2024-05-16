using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels;
using NLog;
using NLog.Fluent;
using System.Diagnostics;


namespace MyBlog.App.Controllers
{
    public class HomeController : Controller
    {
		private readonly IDataDefaultService _dataService;

        public HomeController(IDataDefaultService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IActionResult> Index()
        {

			return View();
        }

        public IActionResult Privacy()
        {
			return View();
        }
	}
}