var path = require('path');
var webpack = require('webpack');
var ExtractTextPlugin = require("extract-text-webpack-plugin");
var autoprefixer = require('autoprefixer');

var pkg = require('../package.json');

module.exports = {
  entry: {
    app: './app/index.js',
    vendor: Object.keys(pkg.dependencies)
  },
  output: {
    path: 'wwwroot',
    filename: '[name].min.js',
    pathinfo: false
  },
  module: {
    // preLoaders: [
    //   {
    //     test: /\.jsx?$/,
    //     loaders:['eslint'],
    //     exclude: /node_modules/
    //   }
    // ],
    loaders: [
      {
        test: /\.js/,
        loader: 'babel-loader',
        query: {
          presets: ['es2015'],
        },
        exclude: /node_modules/
      },
      {
        test: /\.css$/,
        loader: ExtractTextPlugin.extract('style', 'css!postcss')
      },
      {
        test: /\.scss/,
        loader: ExtractTextPlugin.extract('style', 'css!postcss!sass')
      },
      {
        test: /\.less/,
        loader: ExtractTextPlugin.extract('style', 'css!postcss!less')
      }
    ]
  },
  devtool: 'source-map',
  plugins: [
    new webpack.optimize.OccurenceOrderPlugin(),
    new webpack.optimize.CommonsChunkPlugin(
      'vendor',
      '[name].min.js'
    ),
    new webpack.optimize.UglifyJsPlugin({
      compress: {
        warnings: false,
        screw_ie8: true
      }
    }),
    new webpack.DefinePlugin({
      // This affects react lib size
      'process.env': {
        'NODE_ENV': JSON.stringify('production')
      }
    }),
    new ExtractTextPlugin('[name].min.css')
  ],
  postcss: function () {
    return [autoprefixer];
  }
};
