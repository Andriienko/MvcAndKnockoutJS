ko.validation.rules.pattern.message = 'Invalid.';
var settings={
    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: false,
    parseInputAttributes: true,
    messageTemplate: null
};
ko.validation.init(settings, true);
    var VM = {
        message: ko.observable(false),
        person: ko.mapping.fromJS([]),
        // person: ko.observableArray([]),//Array of rows
        firstname: ko.observable().extend({ required: true }),//input value 
        lastname: ko.observable().extend({ required: true }),//input value 
        addData: function () {//Adding data to table by click "Save"
            //this.person = ko.mapping.fromJS(data);
            if (VM.errors().length == 0) {
                this.message(false);
                var per = [
                    { FirstName: this.firstname(), LastName: this.lastname() }
                ];
                ko.mapping.fromJS(per,this);
                this.Savetotxt();
                this.firstname("");
                this.lastname("");
            } else {
                alert('Please check your fields.');
                this.message(true);
            }

        },
        Savetotxt: function () {
            //var persons = [];
            var person = {
                FirstName: this.firstname(),
                LastNAme: this.lastname()
            };
            // persons.push(person);
            $.ajax({
                url: 'Home/Setdata',
                type: 'POST',
                dataType: 'json',
                data: { data: person },
                success: function () {
                    VM.Load();
                }
            });
        },
        Load: function () {
            $.ajax({
                url: "Home/Readdata",
                type: 'GET',
                dataType: 'json',
                success: function (result) {
                    ko.mapping.fromJS(result,VM.person);
                    console.log(result);
                }
            });
        },
        Clear: function () {
            this.person([]);
        },
        Remove: function () {
            if (VM.errors().length == 0) {
                this.message(false);
                var person = {
                    FirstName: this.firstname(),
                    LastNAme: this.lastname()
                };
                $.ajax({
                    url: "Home/Remove",
                    type: 'POST',
                    dataType: 'json',
                    data: { data: person },
                    success: function () {
                        VM.Load();
                    }
                });
            } else {
                alert('Please check your fields.');
                this.message(true);
            }
        }
    };

$(document).ready(function () {
    VM.errors = ko.validation.group(VM);
    ko.applyBindings(VM);
});