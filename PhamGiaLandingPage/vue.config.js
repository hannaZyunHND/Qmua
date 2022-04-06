module.exports = {
    lintOnSave: false,
    runtimeCompiler: true,
    publicPath: '/',
    devServer: {
        proxy: {
            '^/api': {
                target: '/con-ga',
              //  ws: true,
                changeOrigin: true

            }
        }
    }
}
