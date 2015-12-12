var path = require('path');
var webpack = require('webpack');

module.exports = {
  entry: './app/index.js',
  output: {
    path: 'wwwroot',
    filename: 'bundle.js',
    publicPath: 'http://localhost:4000/'
  },
  module: {
    loaders: [
      {
        test: /\.js/,
        loader: 'babel',
        exclude: /node_modules/,
        query: {
          presets: [
            'es2015'
          ]
        }
      },
      {
        test: /\.css/,
        loader: 'style!css!postcss'
      },
      {
        test: /\.scss$/,
        loader: 'style!css!postcss!sass'
      },
      {
        test: /\.less/,
        loader: "style!css!postcss!less"
      }
    ]
  },
  devtool: 'eval-source-map',
  devServer: {
    host: 'localhost',
    port: '4000',
    colors: true,
    historyApiFallback: true,
    hot: true,
    inline: true
  },
  plugins: [
    new webpack.HotModuleReplacementPlugin()
  ]
};