function atualizaListaHorarios() {
    var empresa = document.getElementById('IdEmpresa').value;

    $.ajax({
        url: "/Funcionario/AtualizaListaHorarios",
        data: {
            Empresa: empresa
        },
        type: "GET",
        datatype: "json",
        contenttype: "application/json; charset=utf-8",
        success: function (data) {
            var comboHorarios = document.getElementById('IdHorarioDeExpediente');
            comboHorarios.innerHTML = "";
            alert('teste' + data.count);
            if (data.count > 0) {
                for (var i = 0; i < data.count; i++) {
                    alert('value ' + data[i].value);
                    alert('text ' + data[i].text);
                    //option = $("<option>", { "value": data[i].value }).text(data[i].text);
                    //comboHorarios.append(option);
                    $.each(data.d, function (index, value) {

                        $('#IdHorariosDeExpediente').append("" + value + "");

                    });

                }
            }
        },
        error: function(){
            alert('teste erro');
        }
    });
};
