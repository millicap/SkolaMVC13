$(document).ready(function () {                         //na ucitavanje dokumenta

    $.get("/Drzava/List", function (data, status) {        //get metoda gdje mi dostavlja listu drzava
        $("#listaDrzava").html(data);                      //listu ce smjestiti u div sa id-em #listaDrzava u Index-u
    });
});

$("#btnNovaDrzava").click(function () {                   //na click button Nova drzava
    $("#modalPrikaz").modal('show');                      // otvara se modal sa id-em #modalPrikaz
});


$("#btnSacuvaj").click(function () {                       //na btnSacuvaj klik 

    var data = {
        naziv: $("#txtNaziv").val()                         //uzimam unseseni naziv
    };
   

    $.post("/Drzava/Create", data, function (result, status) {     //post metoda, gadjam kontroler i metodu
     
        if (result.Success) {                                       //ako su ispunjeni uslovi iz kontrolera
            $.get("/Drzava/List", function (drzave) {               //vrati listu drzava
                $("#listaDrzava").html(drzave);                     //u div sa ovim id-em
            });

            $("#txtMessage").html("");                              //ocisti poruku (Unesi podatke npr)
            $("#txtNaziv").val("");                                 //ocisti uneseni naziv
            $("#modalPrikaz").modal('hide');                        //sakrij modal
           

        } else {
            $("#txtMessage").html(result.Message);                  //ako ne, prikazi poruku o gresci u txtMessage
            /*$("#txtMessage").attr("display", "block");*/              
        }
    });
});


$("#btnOtkazi").click(function () {                         //na btnOtkazi
    $("#txtMessage").html("");                              //ocisti poruku (Unesi podatke npr)
    $("#txtNaziv").val("");                                   //ocisti uneseni naziv
});

$("#btnX").click(function () {                          //isto na X
    $("#txtMessage").html("");
    $("#txtNaziv").val("");
});