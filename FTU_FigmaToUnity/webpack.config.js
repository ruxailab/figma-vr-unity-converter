const path = require('path')
const webpack = require('webpack')

const HtmlWebpackPlugin = require('html-webpack-plugin')
const HtmlInlineScriptPlugin = require('html-inline-script-webpack-plugin')

module.exports = (env, argv) => ({
  mode: argv.mode === 'production' ? 'production' : 'development',

  entry: './src/main.js',

  output: {
    path: path.resolve(__dirname)
  },

  performance: {
    hints: 'warning',
    maxEntrypointSize: 10000000,
    maxAssetSize: 10000000
  },

  plugins: [
    new webpack.DefinePlugin({
      global: {}
    }),
    
    new HtmlWebpackPlugin({
      inject: 'body',
      filename: 'ui.html',
      template: './src/index.html',
    }),
    
    new HtmlInlineScriptPlugin({
      htmlMatchPattern: [/ui.html/],
      scriptMatchPattern: [/.js$/],
    })
  ]
})