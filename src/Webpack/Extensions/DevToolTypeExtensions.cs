namespace Webpack.Extensions {

	/// <summary>
	/// Contains extensions methods for <see cref="DevToolType"/>
	/// </summary>
	public static class DevToolTypeExtensions {

		/// <summary>
		/// Returns the webpack required value for the provided <paramref name="devToolType"/>
		/// </summary>
		public static string GetWebpackValue(this DevToolType devToolType) {
			switch (devToolType) {
				case DevToolType.Eval:
					return "eval";
				case DevToolType.CheapEvalSourceMap:
					return "cheap-eval-source-map";
				case DevToolType.CheapSourceMap:
					return "cheap-source-map";
				case DevToolType.CheapModuleEvalSourceMap:
					return "cheap-module-eval-source-map";
				case DevToolType.EvalSourceMap:
					return "eval-source-map";
				case DevToolType.SourceMap:
					return "source-map";
			    case DevToolType.CheapModuleSourceMap:
					return "cheap-module-source-map";
                default:
					return "source-map";
			}
		}
	}
}
