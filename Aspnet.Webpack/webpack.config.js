var path = require('path');
var webpack = require('webpack');

var project = require('./project.json');
var ROOT_PATH = path.resolve(__dirname);

module.exports = {
  entry: path.resolve(ROOT_PATH, 'app', 'index.js'),
  output: {
    path: path.resolve(project.webroot),
    filename: 'bundle.js',
    publicPath: 'http://localhost:3000/js/'
  },
  module: {
    loaders: [
      {
        test: /\.js/,
        loaders: ['babel'],
        exclude: /node_modules/,
      },
      {
        test: /\.scss$/,
        loaders: ["style", "css", "sass"]
      }
    ]
  },
  devtool: 'eval-source-map',
  devServer: {
    contentBase: project.webroot,
    host: "0.0.0.0",
    port: "3000",
    colors: true,
    historyApiFallback: true,
    hot: true,
    inline: true
  },
  plugins: [
    new webpack.HotModuleReplacementPlugin()
  ]
};
