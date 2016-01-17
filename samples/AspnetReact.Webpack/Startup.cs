using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Webpack;

namespace AspnetReact.Webpack {
	public class Startup {
		public Startup(IHostingEnvironment env) {
			// Set up configuration sources.
			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			// Add framework services.
			services.AddMvc();
			services.AddWebpack();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment()) {
				// This is an example of a configuration without an external file
				// In case there is a need to use an external file then remove this section and uncomment the following line
				app.UseWebpack(new WebpackOptions("reactApp/index.js") {
					HandleStyles = true,
					StylesTypes = new List<StylesType> {
						StylesType.Sass
					},
					HandleJsxFiles = true,
					HandleStaticFiles = true,
					StaticFileTypesLimit = 10000,
					StaticFileTypes = new List<StaticFileType> {
						StaticFileType.Png,
						StaticFileType.Jpg
					},
					EnableHotLoading = true
				});
				//app.UseWebpack("webpack_external/webpack.config.js", "bundle.js", new WebpackDevServerOptions());
				app.UseDeveloperExceptionPage();
			}
			else {
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		// Entry point for the application.
		public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
	}
}
