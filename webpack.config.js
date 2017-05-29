var webpack = require('webpack');
var path = require('path');

//Thư mục chứa dự án - các component React
var APP_DIR = path.resolve(__dirname, 'Jsx');

//Thư mục sẽ chứa tập tin được biên dịch
var BUILD_DIR = path.resolve(__dirname, 'wwwroot/js');

module.exports = {
  entry: {
    // Admin app
    'admin/quanLyBaiHoc': './Jsx/admin/QuanLyBaiHoc.jsx',
    // User app
    'user/commentBox': './Jsx/user/CommentBox.jsx',
    'user/exerciseBox': './Jsx/user/ExcerciseBox.jsx'
  },
  output: {
    path: BUILD_DIR,
    filename: '[name].js'
  },
  module: {
    loaders: [
      {
        test: /\.jsx$/,
        include: [APP_DIR],
        exclude: /node_modules/,
        loader: 'babel-loader',
        query: {
          presets: ['es2015', 'react']
        }
      }
    ]
  },
  watch: true
}

// var webpack = require('webpack');
// var path = require('path');

// //Thư mục chứa dự án - các component React
// var APP_DIR = path.resolve(__dirname, 'Areas/Admin/App/Jsx');
// var PUB_DIR = path.resolve(__dirname, 'Public/js/Jsx');

// //Thư mục sẽ chứa tập tin được biên dịch
// var BUILD_DIR = path.resolve(__dirname, 'Public/Dist');

// module.exports = {
//   entry: {
//     bundle: APP_DIR + '/index.jsx',
//     commentBox: PUB_DIR + '/CommentBox.jsx',
//     exerciseBox: PUB_DIR + '/ExcerciseBox.jsx',
//   },
//   output: {
//     path: BUILD_DIR,
//     filename: '[name].js'
//   },
//   module: {
//     loaders: [
//       {
//         test: /\.jsx$/,
//         include: [APP_DIR, PUB_DIR],
//         exclude: /node_modules/,
//         loader: 'babel-loader',
//         query: {
//           presets: ['es2015', 'react']
//         }
//       }
//     ]
//   },
//   watch: true
// }

