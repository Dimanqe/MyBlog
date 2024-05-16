using System.Text.Json;

namespace MyBlog.App.Extentions.ExceptionHandler
{
	public class ErrorDto
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }

		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}
