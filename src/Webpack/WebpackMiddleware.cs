using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Webpack
{
	public class WebpackMiddleware
	{
		RequestDelegate _next;
		WebpackOptions _options;

		public WebpackMiddleware(RequestDelegate next, WebpackOptions options) {
			_next = next;
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
						if (_options.EnableHotLoading) {
							var scriptSource = $"http://{_options.DevServerOptions.Host}:{_options.DevServerOptions.Port}/{_options.OutputFileName}";
							response = response.Replace("</body>", $"<script src=\"{scriptSource}\"></script></body>");
						}
						else {
							response = response.Replace("</body>", $"<script src=\"{_options.OutputFileName}\"></script></body>");
						}
					}
					using (var memStream = new MemoryStream())
					using (var writer = new StreamWriter(memStream)) {
						writer.Write(response);
						writer.Flush();
						memStream.Seek(0, SeekOrigin.Begin);
						await memStream.CopyToAsync(stream);
					}
				}
			}
			else {
				await buffer.CopyToAsync(stream);
			}

		}
	}
}
