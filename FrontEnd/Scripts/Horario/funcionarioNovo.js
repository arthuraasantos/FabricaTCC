function atualizaListaHorarios() {
    var empresa = document.getElementById('IdEmpresa').value;

    $.ajax({
        url: "/Funcionario/AtualizaListaHorarios",
        data: {
            Empresa: empresa
        },
        type: "GET",
        datatype: "json",
        async: true,
        //contenttype: "application/json; charset=utf-8",
        success: function (lista) {
            //alert(lista.length);
            $("#IdHorarioDeExpediente").empty();
            $.each(lista, function (i, item) {
                $("#IdHorarioDeExpediente").append('<option value=" ' + item.Id + ' ">' + item.Text + '</option>');
            })
        }
    })
};
//    .done(function (msg) {
//        //var comboHorarios = document.getElementById('IdHorarioDeExpediente');
//        //comboHorarios.innerHTML = "";
//        var options = "";
//        msg.forEach(function (item) {
//            alert('teste');
//            options += '<option value="' + item.Id + '">' + item.Descricao + '</option>';

//        });
//        $("#IdHorariosDeExpediente").html(options); 
//    })
//};
    //success: function (lista) {
    //    alert(lista.length);
    //    $("#IdHorariosDeExpediente").empty();
    //    for (var i = 0; i < lista.length; i++) {
    //        $("#IdHorariosDeExpediente").append("<option>" + lista[i] + "</option>");
    //    }
    //},
//});

