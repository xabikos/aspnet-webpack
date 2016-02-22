var angular = require('angular');
var ngModule = angular.module('app', []);

require('./styles/app.scss');

require("./todolistController.js")(ngModule);
require("./directives/hello.js")(ngModule);
