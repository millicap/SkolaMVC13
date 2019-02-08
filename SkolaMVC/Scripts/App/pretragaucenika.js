$(function () {
    $('#btn-search').click(function () {   //event na submit //daj mi sve form tagove
      
       // e.preventDefault();    //kada krene submit forme ne radi sto trebas uraditi default-no nego moj dio
        var data = {
            pretraga: $('#pretraga').val(),                            //naziv treba da odg. nazivu prop. koji kontroler prima, u ovom slucaju to je string pretraga, zbog mapiranja
            odjeljenje: $("#odjeljenjeID").val(),
            pol: $("#Pol:checked").val() === undefined ? "" : $("#Pol:checked").val()

        };
        console.log(data);
        $.get("/Statistika/GetData", data, function (result, status) {   //gadjamo kontroler ucenik, metoda pretraga, saljemo parametar pretraga kroz data
            console.log(status);
            $('#result').html(result);


        });
    });           
});