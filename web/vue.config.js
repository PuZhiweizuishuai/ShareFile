
const path = require('path')

function resolve(dir) {
  return path.join(__dirname, dir)
}

const name = '分享你电脑上的文件'

module.exports = {
  productionSourceMap: false,
  // options...
  transpileDependencies: [
    'vuetify'
  ],
  devServer: {
    port: 8080,
    proxy: {
      '/api': {
        target: 'http://127.0.0.1:5000',
        // target: 'http://127.0.0.1:8081/',
        changeOrigin: true,
        ws: true,
        pathRewrite: {
          '^/api': '/api'
        }
      }
    }
  },
  configureWebpack: {
    // provide the app's title in webpack's name field, so that
    // it can be accessed in index.html to inject the correct title.
    name: name,
    resolve: {
      alias: {
        '@': resolve('src')
      }
    }
  }
}

