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
              packageFolder + 'angular-material-icons/angular-material-icons.css',
              srcFolder + 'assets/css/content.css'],
            dest: rootFolder + '/assets/content'
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
              packageFolder + 'angular-material-icons/angular-material-icons.js',
              srcFolder + 'assets/js/satellizer.js',
              packageFolder + 'moment/moment.js',
              packageFolder + 'angular-moment/angular-moment.js',
              srcFolder + 'assets/js/base64.js', ],
            dest: rootFolder + 'assets/lib'
        },
        app: {
            src: [srcFolder + 'app/**/*.js',
                  rootFolder + 'app/templates.js'],
            dest: rootFolder + 'app'
        },
        resources: {
            src: srcFolder + 'app/resources/*.json',
            dest: rootFolder + 'app/resources'
        },
        templates: {
            src: srcFolder + 'app/**/*.html',
            dest: rootFolder + 'app',
            fileName: 'templates.js',
            module: 'abioka',
            root: '/app/'
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
            src: [srcFolder + 'assets/images/**/*.*'],
            dest: rootFolder + 'assets/images'
        },
        fonts: {
            src: [packageFolder + 'font-awesome/fonts/**/*.*'],
            dest: rootFolder + 'assets/fonts'
        }
    };

    config.watch = [srcFolder + 'app/**/*.*'].concat(config.css.src);

    return config;
};