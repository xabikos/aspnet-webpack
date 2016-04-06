using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webpack {
	internal class WebpackMiddleware {
		RequestDelegate _next;
		private readonly ILogger _logger;
		WebPackMiddlewareOptions _options;

		public WebpackMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, WebPackMiddlewareOptions options) {
			_next = next;
			_logger = loggerFactory.CreateLogger<WebpackMiddleware>();
			_options = options;
		}

		public async Task Invoke(HttpContext context) {

			var buffer = new MemoryStream();
			var stream = context.Response.Body;
			context.Response.Body = buffer;

			await _next(context);

			buffer.Seek(0, SeekOrigin.Begin);

			var isHtml = context.Response.ContentType?.ToLower().Contains("text/html");
			if (context.Response.StatusCode == 200 && isHtml.GetValueOrDefault()) {
				using (var reader = new StreamReader(buffer)) {
					var response = await reader.ReadToEndAsync();
					if (response.Contains("</body>")) {
						_logger.LogInformation("A full html page is returned so the necessary script for webpack will be injected");
						var scriptTag = GetScriptTags(_options);
						response = response.Replace("</body>", $"{scriptTag}</body>");
						_logger.LogInformation($"Inject script {scriptTag} as a last element in the body ");
					}
					using (var memStream = new MemoryStream())
					using (var writer = new StreamWriter(memStream)) {
						writer.Write(response);
						writer.Flush();
						memStream.Seek(0, SeekOrigin.Begin);
						context.Response.Headers.Add("Content-Length", memStream.Length.ToString());
						await memStream.CopyToAsync(stream);
					}
				}
			}
			else {
				await buffer.CopyToAsync(stream);
			}

		}

		private static string GetScriptTags(WebPackMiddlewareOptions options) {
			var result = new StringBuilder();
			if (options.EnableHotLoading) {
				options.OutputFileNames.ToList().ForEach(f =>
					result.Append($"<script src=\"http://{options.Host}:{options.Port}/{f}\"></script>")
				);
			} else {
				options.OutputFileNames.ToList().ForEach(f =>
					result.Append($"<script src=\"{f}\"></script>")
				);
			}
			return result.ToString();
		}

	}
}
