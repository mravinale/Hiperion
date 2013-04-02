
// "with" binding modified to be used with jQueryMobile
ko.bindingHandlers.bindWith = {
	'init': function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
		return ko.bindingHandlers["with"].init(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);
	},
	'update': function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
		var handler = ko.bindingHandlers["with"].update(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);
		setTimeout(function () { 
			$(element).trigger("pagecreate"); 
		}, 0);
		return handler;
	}
};

// binding to enable "declarative" navigation between pages
ko.bindingHandlers.bindChangePage = {
	'init': function(element, valueAccessor) {
		$(element).on('click', function() { 
			$.mobile.changePage(valueAccessor());
		});
	}
}