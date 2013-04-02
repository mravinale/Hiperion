

function UserViewModel(id, name, street) {
    this.Id = ko.observable(id);
    this.Name = ko.observable(name);
    this.Address = ko.observable(street);
};

function MainViewModel() {
    var self = this;
    
    self.selectedUser = ko.observable();
    self.Users = ko.observableArray();
    self.newUser = new NewUserViewModel(self.Users);
    self.totalUsers = ko.observable("0");

    self.init = function () {
        $.ajaxSetup({
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            beforeSend: function() {
                $.mobile.showPageLoadingMsg();
            },
            complete: function() {
                $.mobile.hidePageLoadingMsg();
            },
            error: function(jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    },

    self.load = function () {
        $.get("/api/user", function (data) {
            $.each(data, function (i, obj) {
                self.Users.push(new UserViewModel(obj.Id, obj.Name, obj.Address));
            });
        });
    };

    self.update = function () {
        $.post("/api/user", ko.mapping.toJSON(this)).done(function (data) {
            console.log("Data Loaded: " + data);
         });
    };
    
    self.remove = function () {
        var user = this;
	    $.ajax({
	        url: "/api/user/" + ko.mapping.toJSON(user.Id),
	        type: 'DELETE',
	        success:function() {
	            self.Users.remove(user);
	        }
	    });
	};
	
	self.edit = function() {
		self.selectedUser(this);
	};

	self.totalUsers = self.Users.subscribe(function (newValue) {
	    var array = ko.mapping.toJS(newValue);
	    return array.length.toString();
	});
};

function NewUserViewModel(existingUsers) {
    var self = this;
    
	self.Name = ko.observable('');
	
	this.clear = function() {
	    self.Name('');
	};
	this.add = function () {
	    $.post("/api/user", ko.mapping.toJSON(this)).done(function () {
	        existingUsers.push(new UserViewModel(0, self.Name(), ''));
	    });
	};
}

