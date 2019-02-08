$(document).ready(function () {                                                    //kad se ucita html pokrenuce se f-ja iz js
    $(".tabClick").click(function () {
        var data = {
            ucenikId: $("#UcenikId").val(),
            predmetId: $(this).attr("data-predmetId")                           //(this) predmet na koji je kliknuto i uzece atribut data-predmetId toga elementa
        };
        $.get("/UcenikPredmeti/GetOcjene", data, function (result, status) {
            $("#tabContent").html(result);
        });
    });
    $(".tabClick").first().trigger("click");
});