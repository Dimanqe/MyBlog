using NLog;
using System.Net;
using System.Text.Json;

namespace MyBlog.App.Extentions.ExceptionHandler
{
	public class ExceptionHandlingExtention
	{
		private readonly RequestDelegate _requestDelegate;
		private ILogger<ExceptionHandlingExtention> _logger;

		public ExceptionHandlingExtention(RequestDelegate requestDelegate, ILogger<ExceptionHandlingExtention> logger)
		{
			_requestDelegate = requestDelegate;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _requestDelegate(httpContext);
			}
			catch (BadHttpRequestException exception)
			{
				switch (exception.StatusCode)
				{
					case 400:
						await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.BadRequest, $"Произошла ошибка {exception.StatusCode}!");
						break;
					case 403:
						await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.Forbidden, $"Произошла ошибка {exception.StatusCode}!");
						break;
					case 404:
						await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.NotFound, $"Произошла ошибка {exception.StatusCode}!");
						break;
					default:
						await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.InternalServerError, $"Произошла ошибка {exception.StatusCode}!");
						break;
				}
				throw;
			}
		}

		public async Task HandleExceptionAsync(HttpContext context, string exceptionMessage, HttpStatusCode? httpStatusCode, string message)
		{

			HttpResponse response = context.Response;
			response.ContentType = "application/json";
			response.StatusCode = (int)httpStatusCode;

			ErrorDto errorDto = new()
			{
				Message = message,
				StatusCode = (int)httpStatusCode
			};
			
			string result = JsonSerializer.Serialize(errorDto);
			await response.WriteAsJsonAsync(result);
		}
	}
}
