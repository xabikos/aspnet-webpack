using System;
using Microsoft.AspNetCore.Mvc;

namespace Aspnet.Webpack.Controllers {
	public class HomeController : Controller {
		public IActionResult Index() {
			ViewBag.Message = "This is some content from the controller action";
			return View();
		}

		public IActionResult Error() {
			return View();
		}
	}
}
