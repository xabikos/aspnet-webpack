using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace Webpack {
	public static class WebpackExtensions {

		/// <summary>
		/// The extension method that used to add webpack functionality to the application. It should be used only during development.
		/// </summary>
		public static IApplicationBuilder UseWebpack(this IApplicationBuilder app, WebpackOptions options) {
			var webpack = app.ApplicationServices.GetService<IWebpack>();
			webpack.Execute(options);
			app.UseMiddleware<WebpackMiddleware>(options);
			return app;
		}
	}
}
