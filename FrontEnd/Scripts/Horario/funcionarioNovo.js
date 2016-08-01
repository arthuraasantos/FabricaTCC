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
        success: function (lista) {
            if (lista.length > 0) {
                $("#IdHorarioDeExpediente").empty();
                $.each(lista, function (i, item) {
                    $("#IdHorarioDeExpediente").append('<option value=" ' + item.Value + ' ">' + item.Text + '</option>');
                });
            }
        }
    })
};

