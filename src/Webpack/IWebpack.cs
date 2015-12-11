
namespace Webpack {
	/// <summary>
	/// Basic contract interface for Webpack execution environment
	/// </summary>
	public interface IWebpack {
		/// <summary>
		/// Executes the webpack or webpack-dev-server based on the provided <paramref name="options"/>
		/// </summary>
		/// <param name="options">The webpack configuration object</param>
		void Execute(WebpackOptions options);
	}
}
