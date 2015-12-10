$(function () {
 
    $("#btnlogin").click(function(event) {
    
        document.getElementById('btnlogin').disabled;
            event.preventDefault();
        $.ajax({
                       //pegando a url apartir da action do form
            url: "/Login/LoginValidate",
            data: { email: $("input[type=email][name=Email]").val(), password: $("input[type=password][name=Password]").val() },
            type: 'GET',
            async: false,
            context: jQuery('#resultado')
        }).success(function(response){
                if (response.Message) {
                    ShowWarning(response.Message);
                }
                else {
                    doLogin();
                }
                
                document.getElementById('btnlogin').disabled = false;

        }).error(function(response){
                ShowDanger("Erro ao validar o login. Mensagem: "+response.Message);
        });     
    });

    function doLogin(){
    $.ajax({
            url: "/Login/Autenticar",
            data: {
                email: $("input[type=email][name=Email]").val(),
                password: $("input[type=password][name=Password]").val(),
                remember: $("input[type=checkbox][name=Remember]").val()
            },
            type: "POST",
            async: false,
            context: jQuery('#resultado'),
                // erro ao logar
            error: function(response){
                    ShowDanger("Erro ao fazer o login. Mensagem: "+response.Message);
            },
    }).success(function(response){
        window.location.href = "Home/Index";
    });
}


});



