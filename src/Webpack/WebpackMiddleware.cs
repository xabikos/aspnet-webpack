﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Webpack {
	public class WebpackMiddleware {
	    private readonly RequestDelegate _next;
		private readonly ILogger _logger;
	    private readonly WebPackMiddlewareOptions _options;

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
					if (response.Contains("</body>"))
                    {
                        _logger.LogInformation("A full html page is returned so the necessary script for webpack will be injected");
                        response = AddEachScriptTagToHtml(response);
                    }
                    using (var memStream = new MemoryStream())
					using (var writer = new StreamWriter(memStream)) {
						writer.Write(response);
						writer.Flush();
						memStream.Seek(0, SeekOrigin.Begin);
						context.Response.Headers["Content-Length"] = memStream.Length.ToString();
						await memStream.CopyToAsync(stream);
					}
				}
			}
			else {
				await buffer.CopyToAsync(stream);
			}
			// TODO this is a workaround as there is a related bug in Kestrel which will is fixed and will be published at a later point
			// https://github.com/aspnet/KestrelHttpServer/issues/940
			context.Response.Body = stream;
		}

        private string AddEachScriptTagToHtml(string response)
        {
            foreach (var fileName in _options.OutputFileNames)
            {
                string scriptTag;
                if (_options.EnableHotLoading)
                {
                    scriptTag = $"<script src=\"http://{_options.Host}:{_options.Port}/{fileName}\"></script>";
                    response = response.Replace("</body>", $"{scriptTag}</body>");
                }
                else
                {
                    scriptTag = $"<script src=\"{fileName}\"></script>";
                    response = response.Replace("</body>", $"{scriptTag}</body>");
                }
                _logger.LogInformation($"Inject script {scriptTag} as a last element in the body ");
            }

            return response;
        }
    }
}
