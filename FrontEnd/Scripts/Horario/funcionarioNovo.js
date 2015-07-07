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
        contenttype: "application/json; charset=utf-8",
    })
    .done(function (msg) {
        var comboHorarios = document.getElementById('IdHorarioDeExpediente');
        comboHorarios.innerHTML = "";
        var options = "";
        msg.forEach(function (item) {
            alert('teste');
            options += '<option value="' + item.Id + '">' + item.Descricao + '</option>';

        });
        $("#IdHorariosDeExpediente").html(options); 
    })
};



    //success: function (lista) {
    //    alert(lista.length);
    //    $("#IdHorariosDeExpediente").empty();
    //    for (var i = 0; i < lista.length; i++) {
    //        $("#IdHorariosDeExpediente").append("<option>" + lista[i] + "</option>");
    //    }
    //},
    //error: function () {
    //    alert('teste erro');
    //}
    ////success: function (data) {
    //    var comboHorarios = document.getElementById('IdHorarioDeExpediente');
    //    comboHorarios.innerHTML = "";
    //    alert('teste' + data.count);
    //    if (data.count > 0) {
    //        for (var i = 0; i < data.count; i++) {
    //            alert('value ' + data[i].value);
    //            alert('text ' + data[i].text);
    //            //option = $("<option>", { "value": data[i].value }).text(data[i].text);
    //            //comboHorarios.append(option);
    //            $.each(data.d, function (index, value) {

    //                $('#IdHorariosDeExpediente').append("" + value + "");

    //            });

    //        }
    //    }
    //},
       
//});

