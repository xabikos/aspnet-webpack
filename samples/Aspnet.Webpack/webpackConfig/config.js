var path = require('path');
var webpack = require('webpack');

var project = require('../project.json');
var ROOT_PATH = path.resolve(__dirname);

module.exports = {
  entry:'./app/index.js',
  output: {
    path: path.resolve(project.webroot),
    filename: 'bundle.js',
    publicPath: 'http://localhost:4000/'
  },
  module: {
    loaders: [
      {
        test: /\.js/,
        loaders: ['babel'],
        exclude: /node_modules/,
      },
      {
        test: /\.css$/,
        loaders: ["style", "css"]
      },
      {
        test: /\.scss$/,
        loaders: ["style", "css", "sass"]
      },
      {
        test: /\.less$/,
        loaders: ["style", "css", "less"]
      }
    ]
  },
  devtool: 'eval-source-map',
  devServer: {
    contentBase: project.webroot,
    host: "0.0.0.0",
    port: "4000",
    colors: true,
    historyApiFallback: true,
    hot: true,
    inline: true
  },
  plugins: [
    new webpack.HotModuleReplacementPlugin()
  ]
};