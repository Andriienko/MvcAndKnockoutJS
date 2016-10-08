var VM = {
    person: ko.observableArray([]),//Array of rows
    firstname: ko.observable(),//input value 
    lastname: ko.observable(),//input value 
    FirstName: ko.observable(),//value in the table
    LastName: ko.observable(),//value in the table
    addData: function () {//Adding data to table by click "Save"
        this.FirstName(this.firstname());
        this.LastName(this.lastname());
        this.person.push({ FirstName: VM.FirstName(), LastName: VM.LastName() });
        this.firstname("");
        this.lastname("");
    }
};
ko.applyBindings(VM);
$(document).ready(function () {
    //Saving data from table to TXT in JSON format using AJAX by JQuery
    $('#submit').click(function (e) {
        e.preventDefault();
        var persons = [];
        var table = $("table tbody");
        table.find('tr').each(function (i, el) {
            var $tds = $(this).find('td'),
                fn = $tds.eq(0).text(),
                ln = $tds.eq(1).text();
            var per = {
                FirstName: fn,
                LastName:ln
            };
            persons.push(per);
        });
        $.ajax({
            url: 'Home/Setdata',
            type: 'POST',
            dataType: 'json',
            data: { data: persons },
            success: function () {}
        });
    });
    //Loading data from TXT to table in JSON format using AJAX by JQuery
    $('#load').click(function(e) {
        $.ajax({
            url: "Home/Readdata",
            type: 'GET',
            dataType:'json',
            success: function (result) {
                VM.person(result);//Update data in table
               // console.log(result);
            } });
    });
    $('#clear').click(function (e) {
        var clr = [];
                VM.person(clr);//Clear data in table
    });
});