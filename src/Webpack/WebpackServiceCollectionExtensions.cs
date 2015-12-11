using Microsoft.Extensions.DependencyInjection;

namespace Webpack {

	/// <summary>
	/// Handles registering Webpack services in the ASP.NET <see cref="IServiceCollection"/>.
	/// </summary>
	public static class WebpackServiceCollectionExtensions {
		/// <summary>
		/// Registers all services required for Webpack
		/// </summary>
		public static IServiceCollection AddWebpack(this IServiceCollection services) {
			services.AddSingleton<IWebpack, Webpack>();
			return services;
		}
	}
}
