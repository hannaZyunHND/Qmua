const path = require('path');
const webpack = require('webpack');
const merge = require('webpack-merge');
const UglifyJSPlugin = require('uglifyjs-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const VueLoaderPlugin = require('vue-loader-plugin');

module.exports = (env) => {
    const isDevBuild = !((env && (env.prod || env.production)) || env == 'production');
    console.log('webpack start build', env, isDevBuild);
    const sharedConfig = () => ({
        devtool: false,
        stats: {
            modules: false
        },
        performance: {
            hints: false
        },
        resolve: { extensions: ['.js', '.vue'] },
        output: {
            filename: '[name].js',
            publicPath: '/dist/'
        },
        module: {
            rules: [
                {
                    test: /\.vue$/,
                    loader: 'vue-loader'
                },
                {
                    test: /\.js$/,
                    loader: 'babel-loader',
                    include: __dirname,
                    exclude: [/node_modules/, /wwwroot/]
                },
                {
                    test: /\.css$/,
                    loader: "vue-style-loader!css-loader"
                }
            ]
        },
        plugins: [
            new webpack.LoaderOptionsPlugin({
                minimize: true,
                debug: false
            }),
            new UglifyJSPlugin({
                sourceMap: false
            }),
            new VueLoaderPlugin()
        ], optimization: {
            splitChunks: {
                chunks: 'async',
                minSize: 30000,
                minChunks: 1,
                maxAsyncRequests: 5,
                maxInitialRequests: 3,
                automaticNameDelimiter: '~',
                name: true,
                cacheGroups: {
                    vendors: {
                        test: /[\\/]node_modules[\\/]/,
                        priority: -10
                    },
                    default: {
                        minChunks: 2,
                        priority: -20,
                        reuseExistingChunk: true
                    }
                }
            }
        }
    });

    const outputDir = './wwwroot/dist';
    const clientBundleConfig = merge(sharedConfig(), {
        entry: { 'main-client': './ClientApp/entry-client.js' },
        output: {
            path: path.join(__dirname, outputDir)
        },
        plugins: [
            new CleanWebpackPlugin(['wwwroot/dist/*.*'])
        ]
    });

    const serverBundleConfig = merge(sharedConfig(), {
        target: 'node',
        entry: { 'main-server': './ClientApp/entry-server.js' },
        output: {
            libraryTarget: 'commonjs2',
            path: path.join(__dirname, outputDir)
        },
        module: {
            rules: [
                {
                    test: /\.json?$/,
                    loader: 'json-loader',
                    exclude: [/lang/, /wwwroot/, /node_modules/]
                }
            ]
        },
    });

    return [clientBundleConfig, serverBundleConfig];
}
//webpack --color -p --progress
