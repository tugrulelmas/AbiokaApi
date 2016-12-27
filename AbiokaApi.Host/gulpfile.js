var gulp = require('gulp'),
    config = require('./gulp.config')({ lazy: true }),
    pkg = require('./package.json'),
    rimraf = require("gulp-rimraf"),
    inject = require('gulp-inject'),
    runSequence = require('run-sequence'),
    angularFilesort = require('gulp-angular-filesort'),
    ngAnnotate = require('gulp-ng-annotate'),
    concat = require('gulp-concat'),
    cleanCSS = require('gulp-clean-css'),
    uglify = require('gulp-uglify'),
    header = require('gulp-header'),
    rev = require('gulp-rev'),
    watch = require('gulp-watch'),
    templateCache = require('gulp-angular-templatecache'),
    htmlmin = require('gulp-htmlmin'),
    jshint = require("gulp-jshint");

gulp.task('copy', function () {
    gulp.src([config.views.src, '!' + config.index.src])
             .pipe(header('\ufeff'))
             .pipe(gulp.dest(config.views.dest));

    gulp.src(config.resources.src)
         .pipe(gulp.dest(config.resources.dest));

    gulp.src(config.fonts.src)
         .pipe(gulp.dest(config.fonts.dest));

    return gulp.src(config.images.src)
          .pipe(gulp.dest(config.images.dest));
});

var createOptions = function (name) {
    return { name: name, addRootSlash: false }
};

gulp.task('clean', function (cb) {
    return gulp.src([config.views.dest, config.css.dest, config.lib.dest, config.app.dest, config.images.dest, config.fonts.dest, config.templates.dest], { read: false })
            .pipe(rimraf());
});

gulp.task('templateCache', function () {
    var baseFn = function (file) { return './' + file.relative; }

    return gulp.src(config.templates.src)
      .pipe(htmlmin({ collapseWhitespace: true }))
      .pipe(templateCache(config.templates.fileName, { module: config.templates.module, root: config.templates.root, base: baseFn }))
      .pipe(gulp.dest(config.templates.dest));
});

gulp.task('inject', ['copy', 'templateCache'], function () {
    var css = gulp.src(config.css.src)
              .pipe(gulp.dest(config.css.dest));

    var lib = gulp.src(config.lib.src)
         .pipe(gulp.dest(config.lib.dest));

    var app = gulp.src(config.app.src)
              .pipe(jshint())
              .pipe(jshint.reporter())
              .pipe(ngAnnotate())
              .pipe(gulp.dest(config.app.dest))
              .pipe(angularFilesort());

    return gulp.src(config.index.src)
      .pipe(inject(css, createOptions()))
      .pipe(inject(lib, createOptions('lib')))
      .pipe(inject(app, createOptions('app')))
      .pipe(header('\ufeff'))
      .pipe(gulp.dest(config.index.dest));
});

gulp.task('inject:dist', function () {
    var css = gulp.src(config.css.src)
              .pipe(concat("content.min.css"))
              .pipe(cleanCSS())
              .pipe(header(config.banner, { pkg: pkg }))
              .pipe(rev())
              .pipe(gulp.dest(config.css.dest));

    var lib = gulp.src(config.lib.src)
         .pipe(concat("lib.min.js"))
         .pipe(uglify())
         .pipe(header(config.banner, { pkg: pkg }))
         .pipe(rev())
         .pipe(gulp.dest(config.lib.dest));

    var app = gulp.src(config.app.src)
              .pipe(jshint())
              .pipe(jshint.reporter())
              .pipe(ngAnnotate())
              .pipe(angularFilesort())
              .pipe(concat("app.min.js"))
              .pipe(uglify())
              .pipe(header(config.banner, { pkg: pkg }))
              .pipe(rev())
              .pipe(gulp.dest(config.app.dest));

    return gulp.src(config.index.src)
      .pipe(inject(css, createOptions()))
      .pipe(inject(lib, createOptions('lib')))
      .pipe(inject(app, createOptions('app')))
      .pipe(header('\ufeff'))
      .pipe(gulp.dest(config.index.dest));
});

gulp.task('default', ['inject'], function () {
    watch(config.watch, function () {
        gulp.start('inject');
    });
});

gulp.task('clean-templates', function () {
    return gulp.src(config.templates.dest + '/' + config.templates.fileName, { read: false })
            .pipe(rimraf());
});

gulp.task('dist', function () {
    runSequence('clean', 'copy', 'templateCache', 'inject:dist', 'clean-templates');
});