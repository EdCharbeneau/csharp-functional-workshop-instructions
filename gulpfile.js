var gulp = require( "gulp" ),
	concat = require( "gulp-concat" ),
	fileInclude = require( "gulp-file-include" ),
	marked = require( "marked" ),
	sass = require( "gulp-sass" ),
	watch = require( "gulp-watch" ),
	webServer = require( "gulp-webserver" );

gulp.task( "default", [ "build", "server" ], function() {
	gulp.watch( "./src/**/*", [ "build" ]);
});

gulp.task( "build", [ "include", "sass", "scripts" ]);

gulp.task( "server", function() {
	return gulp.src( "./" )
		.pipe(webServer({
			livereload: {
				enable: true,
				filter: function(fileName) {
					return fileName.match(/index.html$/);
				}
			},
			port: "3027",
			open: "http://localhost:3027"
		}));
});

gulp.task( "include", function() {
	return gulp.src( "./src/index.html" )
		.pipe( fileInclude({
			prefix: "@@",
			filters: {
				markdown: marked
			}
		}))
		.pipe( gulp.dest( "./" ) );
});

gulp.task( "sass", function () {
	return gulp.src( "./src/scss/*.scss" )
		.pipe( sass() )
		.pipe( gulp.dest( "./css" ) );
});

gulp.task( "scripts", function() {
	return gulp.src([
			"src/scripts/fastclick.js",
			"src/scripts/jquery.js",
			"src/scripts/highlight.pack.js",
			"src/scripts/app.js"
		])
		.pipe( concat( "built.js" ) )
		.pipe( gulp.dest( "./scripts/" ) );
});
