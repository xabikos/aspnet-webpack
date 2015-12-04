using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

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
