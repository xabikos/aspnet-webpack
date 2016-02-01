ASP.NET WEBPACK
=============

##Webpack 
[Webpack](https://webpack.github.io/) is a well known module bundler that could be used cross platform and requires only a node environment in order to use it.
This repo contains the nuget package that enables the seamless usage of webpack combined with an ASP.NET 5 application.

##Why to use it
Webpack offers some unique features regarding static asset management and gives us tremendous flexibility. Some of the most notable features are:
- Write modular JavaScript code by using module system 
- Use node packages inside browser
- Use ES6 features through [Babel](https://babeljs.io/)
- Handle all the possible styles (css, scss, less) and even more
- Handle images and other external files (e.g. fonts)
- Prepare the production bundles in an advanced way

###Powerful development environment and live reload cross platform with webpack-dev-server
One of the most powerful tools that comes with webpack is the [webpack-dev-server](http://webpack.github.io/docs/webpack-dev-server.html) that can be used for live reload of all the assets that webpack handles.
The development server could be used as a standalone tool and serve both html and static assets. On the other hand 
can be combined with an existing application and serve only the static assets. In both cases with the proper configuration we can have live reload.  

###The two development workflows
There are two development workflows that we can use with webpack.

####Bundle creation
In that scenario every time we execute webpack it will create the bundles in the configured path.
No live reload is enabled so after every change we need to execute webpack again and refresh the page.

####Bundle creation and live reload
In this case every time we launch our application the webpack dev server is started and creates all the required bundles in the memory.
The webpack dev server establishes a web socket connection with our application and every time a JavaScript or a style file changes an automatic refresh is triggered in our browser.
In case of style changes the page is not refreshed as all the styles are embedded in style elements in the head part of the page.
In case of a [React.js](http://facebook.github.io/react/) application when a change in a component is happened the page does not refresh either as you can see in the corresponding sample. 

In both scenarios above we don't have to add manually the script tags in the layout file or somewhere else as the library itself will do that for us through the webpack middleware.

##Installation and Usage
In order to install the package you need to execute in a package manager console in Visual Studio `Install-Package Webpack`
or edit manually the project.json file and add in dependencies section the item `"Webpack": "*"`.

After successful installation there are available three extensions methods we could use based on the scenario we need.

###Register services
First step is to add the `services.AddWebpack()` section in the ConfigureServices method in order for the library
to register the required dependencies.

```cs
public void ConfigureServices(IServiceCollection services) {
	services.AddMvc();
	services.AddWebpack();
}
```

###Usage without external configuration file
Webpack can be used either as cli and pass all the required configuration as command line arguments or create a separate [configuration file](ttps://webpack.github.io/docs/configuration.html).
In case we don't want to create a separate configuration file the library provides a way to configure webpack for the most common scenarios during development.
An example configuration could be like:

```cs
app.UseWebpack(new WebpackOptions() {
	StylesTypes = new List<StylesType>() {
		StylesType.Css,
		StylesType.Sass,
		StylesType.Less,
	}
});
```
In this example we just use the default configuration values and no live reload will happen.
The available options are pretty self explenatory and it shouldn't be any problem provide the appropriate configuration.

A more complete configuration with live reload follows:
 ```cs
app.UseWebpack(new WebpackOptions("reactApp/index.js", "appBundle.js", true) {
	StylesTypes = new List<StylesType>() {
		StylesType.Css,
		StylesType.Sass,
		StylesType.Less,
	},
	EnableHotLoading = true,
	DevServerOptions = new WebpackDevServerOptions("localhost", 5000)
});
```
In this case we declare that the entry point of our application is the `index.js` that lives inside the folder `reactApp`
and the output file will be the `appBundle.js` and will be placed inside the web root folder that is returned from the  `IHostingEnvironment.WebRootPath` property.
In case we want to place this file inside the a different location e.g. `wwwroot/js/appBundle.js` then we need to provide the full relative path.
The entry point file is a relative path to the project's folder. At the end the webpack dev server will started in the provided address and port.

###Usage with external configuration file
The available options above are quite limited compared to what is possible through an external webpack configuration file.
In that case there are two other overloads of the UseWebpack method we can use according to the workflow. 
If the live reload is necessary then all of the parameters should be provided as in the following example:

 ```cs
app.UseWebpack("webpack/webpack.development.js", "bundle.js", new WebpackDevServerOptions("localhost", 3000));
});
```
The first parameter is the external configuration file the second is the name of the output file and the last one is the parameters for the development server.
In this case we have a small duplication of the configuration in the external config file, webpack.development.js in our case and the provided parameters.
The output file should be exactly the same as the one in output section filename is configuration file and the same applies for the development server options.
This is required in order the webpack middleware to inject the correct script in the html.

##Production bundles
There is no automation for production bundles and it must be handled according to each aplication's needs.
In the Aspnet.Webpack sample in the webpack folder there is an example of a production configuration for the project.

##Known issues
- Not compatible with IIS Express

   In Windows environment the IIS Express is not supported yet. You need to use the Kestrel web server, web command in most projects.
   
- Webpack folder
   
   Even when we use webpack without an external configuration file the library creates a folder witn name webpack and puts there a file with name webpack.dev.js.
   This is happening as there is no way to exclude some folders from babel-loader only by using the cli. This folder can be ignored and not changed manually.

- Multiple entry points
   
   The predefined confguration does not support multiple entry points at the moment.

##Notes
The library has been tested in both windows 10 and OSX El Capitan. It would be great if someone could give it a try in a Linux environment.

##Your feedback
This library came out after I was involved in a project that uses asp.net 5 and Reactjs in the same application. Your feedback is very important in order to cover more scenarios.
Please open issues on possible bugs or other ideas of how it could be improved.
