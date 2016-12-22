module.exports = function () {

    var rootFolder = './';
    var srcFolder = 'src/';
    var packageFolder = 'node_modules/';

    var config = {
        rootFolder: rootFolder,
        banner: ['/*!\n',
        ' * abioka- <%= pkg.title %> v<%= pkg.version %> (<%= pkg.homepage %>)\n',
        ' * Copyright 2016-' + (new Date()).getFullYear(), ' <%= pkg.author %>\n',
        ' * Licensed under <%= pkg.license.type %> (<%= pkg.license.url %>)\n',
        ' */\n',
        ''
        ].join(''),
        css: {
            src: [packageFolder + 'angular-material/angular-material.css',
              packageFolder + 'angular-material-data-table/dist/md-data-table.css',
              srcFolder + 'css/content.css'],
            dest: rootFolder + '/content'
        },
        lib: {
            src: [packageFolder + 'angular/angular.js',
              packageFolder + 'angular-animate/angular-animate.js',
              packageFolder + 'angular-aria/angular-aria.js',
              packageFolder + 'angular-cookies/angular-cookies.js',
              packageFolder + 'angular-resource/angular-resource.js',
              packageFolder + 'angular-ui-router/release/angular-ui-router.js',
              packageFolder + 'angular-messages/angular-messages.js',
              packageFolder + 'angular-material/angular-material.js',
              packageFolder + 'angular-material-data-table/dist/md-data-table.js',
              packageFolder + 'moment/moment.js',
              packageFolder + 'angular-moment/angular-moment.js',
              srcFolder + 'js/base64.js',],
            dest: rootFolder + 'lib'
        },
        app: {
            src: srcFolder + 'app/**/*.js',
            dest: rootFolder + 'app'
        },
        resources: {
            src: srcFolder + 'app/resources/*.json',
            dest: rootFolder + 'resources'
        },
        templates: {
            src: srcFolder + 'templates/**/*.html',
            dest: rootFolder + 'templates'
        },
        views: {
            src: srcFolder + 'Views/**/*',
            dest: 'Views',
        },
        index: {
            src: srcFolder + 'Views/Shared/_Layout.cshtml',
            dest: 'Views/Shared'
        },
        images: {
            src: [srcFolder + 'images/**/*.*'],
            dest: rootFolder + 'images'
        },
        fonts: {
            src: [packageFolder + 'font-awesome/fonts/**/*.*'],
            dest: rootFolder + 'fonts'
        }
    };

    config.watch = [config.app.src, config.templates.src, config.resources.src].concat(config.css.src);

    return config;
};