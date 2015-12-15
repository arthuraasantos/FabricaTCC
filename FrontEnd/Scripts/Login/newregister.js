function register(){
    $("alert alert-info").hide();
    document.getElementById("btnlogin").disabled = true;


    $("#btnlogin").click(function(event) {
        $(".loader").show();

        
        $.ajax({
            url: "/Login/Register",
            data: { email: $("input[type=email][name=Email]").val(), password: $("input[type=password][name=Password]").val() },
            type: 'GET',
            async: true,
            context: jQuery('#resultado')
        }).success(function(response){
                if (response.Message) {
                    ShowWarning(response.Message);
                    document.getElementById("btnlogin").disabled = false;
                    $(".pn-login-loader").hide();
                }
                else {
                    doLogin();
                }

        }).error(function(response){
                ShowDanger("Erro ao validar o login. Mensagem: "+response.Message);
                document.getElementById("btnlogin").disabled = false;
                $(".pn-login-loader").hide();                
        });     
    });
}


