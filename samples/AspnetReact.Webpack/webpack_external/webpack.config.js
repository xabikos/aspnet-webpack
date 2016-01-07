var path = require('path');
var webpack = require('webpack');

var project = require('../project.json');
var ROOT_PATH = path.resolve(__dirname);

module.exports = {
  entry: path.resolve('reactApp', 'index.js'),
  output: {
    path: path.resolve(project.webroot),
    filename: 'bundle.js',
    publicPath: 'http://localhost:4000/'
  },
  resolve: {
    extensions: ['', '.js', '.jsx'],
  },
  module: {
    loaders: [
      {
        test: /\.jsx?$/,
        loader: 'babel',
        exclude: /node_modules/,
        query: {
          presets: ['es2015', 'react']
        }
      },
      {
        test: /\.scss$/,
        loaders: ["style", "css", "sass"]
      }
    ]
  },
  devtool: 'source-map',
  devServer: {
    host: "0.0.0.0",
    port: "4000",
    colors: true,
    historyApiFallback: true,
    hot: true,
    inline: true,
    stats: 'errors-only'
  },
  plugins: [
    new webpack.HotModuleReplacementPlugin()
  ]
};
