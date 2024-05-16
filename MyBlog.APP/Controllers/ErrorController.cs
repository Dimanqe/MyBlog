using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NLog;



namespace MyBlog.App.Controllers
{
    public class ErrorController : Controller
    {
        public ErrorController(){ }

		[Route("Error/Default")]
		public IActionResult Index(int statusCode)
		{
			if (statusCode == 400 || statusCode == 403 || statusCode == 404)
            {
				var feature = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

				return View(statusCode.ToString());
			}

            else return View("500");
		}

		/// <summary>
		/// Status code 400
		/// </summary>
		/// <returns>Ошибка 400</returns>
		[Route("Error/400")]
		[HttpGet]
        public IActionResult GetException400()
        {
            try
            {
				throw new HttpRequestException("400");
            }
            catch
            {
                return View("400");
            }
        }

        /// <summary>
        /// Status code 403
        /// </summary>
        /// <returns>Ошибка 403</returns>
        [Route("Error/403")]
        [HttpGet]
        public IActionResult GetException403()
        {
            try
            {
				throw new HttpRequestException("403");
            }
            catch
            {
                return View("403");
            }
        }

        /// <summary>
        /// Status code 404
        /// </summary>
        /// <returns>Ошибка 404</returns>
        [Route("Error/404")]
        [HttpGet]
        public IActionResult GetException404()
        {
            try
            {
				throw new HttpRequestException("404");
            }
            catch
            {
                return View("404");
            }
        }

		/// <summary>
		/// Status code 500
		/// </summary>
		/// <returns>Ошибка 500</returns>
		[Route("Error/500")]
		[HttpGet]
		public IActionResult GetException500()
		{
			try
			{
				throw new HttpRequestException("500");
			}
			catch
			{
				return View("500");
			}
		}

	}
}