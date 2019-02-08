$(document).ready(function () {

    $.get("/RandomUcenik/GetRandomUcenik", function (data, status) {     //kontroler RandomUcenik i f-ja GetRandomUcenik
        $("#ucenik").html(data);                                          //na div ucenik dodajemo html sa servera tj partial
    });
});


$("#btnPrikaziRandomModal").click(function () {
    console.log("fdsff");
    $.get("/RandomUcenik/GetRandomUcenik", function (data, status) { 
    $("#randomModal").html(data);   //dobavlja podatke
        $('#modelPrikaz').modal('show');   //prikazuje modal
    });
});