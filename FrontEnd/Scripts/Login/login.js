$(function () {
    $(".btn-success").click(function () {
         $.ajax({
            url: "/Login/LoginValidate",
            data: {
                email: $("input[type=email][name=Email]").val(),
                password: $("input[type=password][name=Password]").val()
            },
            type: "GET",
            datatype: "json",
            async: true ,
            sucess: function (response) {
                alert("chegou aqui");
                if (response.IsValid) {
                    // Faz login
                    doLogin();
                }
                else
                {
                    // exibir div de erro
                    if (response.Type == "Warning") {
                        ShowWarning(response.Message)
                    }
                    else if (data.Type == "Error") {
                        ShowDanger(response.Message)
                    }
                }
            }, 
            error: function(response){
                alert("teste error"+ response);
            }
        });
    });
});

function doLogin(){
    alert("chegou no dologin");

    $.ajax({
            url: "/Login/Autenticar",
            data: {
                email: $("input[type=email][name=Email]").val(),
                password: $("input[type=password][name=Password]").val()
            },
            type: "GET",
            datatype: "json",
            contenttype: "application/json; charset=utf-8",
        }).fail(function(response){
            // erro ao logar
            ShowDanger(response.Message);
    });
    
    alert("terminou sem dar erro");
}


