(function() {
	"use strict";

	FastClick.attach(document.body);

	function isMobile() {
		return $(window).width() <= 700;
	}

	// TODO: Don't do this in JS. Ugh.
	if (isMobile()) {
		$("body").addClass("no-toc");
	}

	// Build the TOC
	var toc = $("#toc-padding");
	$(".chapter").each(function(chapterNumber) {
		$(this).find("h2").each(function() {
			$(this)
				.attr("id", "chapter" + chapterNumber)
				.prepend("<strong>" + chapterNumber + ". </strong>");
			toc.append("<h5><a href='#chapter" + chapterNumber + "'>" +
				$(this).html() + "</a></h5>");
		});

		$(this).find("h3").each(function(sectionNumber) {
			var hash = chapterNumber + "." + sectionNumber + "";
			$(this)
				.attr("id", "chapter" + hash)
				.prepend("<strong>" + hash + ". </strong>");
			toc.append("<h6><a href='#chapter" + chapterNumber + "." + sectionNumber + "'>" +
				$(this).html() + "</a></h6>");
		});
	});

	// Build the exercise sections
	$(".exercise-start").each(function() {
		var exerciseDiv = $("<div class='exercise'></div>");
		$(this).before(exerciseDiv);
		$(this).nextUntil(".exercise-end").addBack().appendTo(exerciseDiv);
	});
	$(".exercise-end").remove();

	$("#toggle-toc").on("click", function() {
		$("body").toggleClass("no-toc");
		return false;
	});
	$("#toc").on("click", "a", function() {
		if (isMobile()) {
			$("body").addClass("no-toc");
		}
	});

	hljs.initHighlightingOnLoad();

	// This is horrible, but detecting clipboard support without UA sniffing is
	// basically impossible at the moment. We can (hopefully) whitelist Firefox
	// after the upcoming Firefox 41 release.
	// https://gist.github.com/jonrohan/81085b119d16cdd7868a
	if (navigator.userAgent.match(/Chrome/)) {
		// Add copy buttons to all pre tags in exercises
		$(".exercise pre").each(function() {
			// Pre tags in exercises can remove the code button by including a div
			// with the no-copy-button class name before them.
			if ($(this).prev().hasClass("no-copy-button")) {
				return;
			}
			$(this).prepend("<button class='copy-button' title='Copy to clipboard'>Copy</button>");
		});
	}
	// See https://developers.google.com/web/updates/2015/04/cut-and-copy-commands?hl=en
	$(".copy-button").on("click", function() {
		window.getSelection().removeAllRanges();
		var codeElement = $(this).parent().find("code")[0];
		var range = document.createRange();
		range.selectNode(codeElement);
		window.getSelection().addRange(range);
		document.execCommand("copy");
		window.getSelection().removeAllRanges();
		$(this).blur();
	});
}());
