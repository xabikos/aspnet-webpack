using System.Collections.Generic;
using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;
using System.IO;

namespace Webpack {
	public static class HtmlHelperExtensions
	{
		public static IHtmlContent InitWebpackScript(this IHtmlHelper helper) {
			var options = WebpackOptions.CurrentOptions;
			var tag = new TagBuilder("script");
			tag.Attributes.Add(new KeyValuePair<string, string>("src", options.OutputFileName));
			return tag;
		}
	}
}
