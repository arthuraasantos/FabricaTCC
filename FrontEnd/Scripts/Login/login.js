$(function () {
 
    $("#btnlogin").click(function(event) {
        document.getElementById("btnlogin").disabled = true;
        $(".pn-login-span-loader").show();

        //event.preventDefault();
        $.ajax({
            url: "/Login/LoginValidate",
            data: { email: $("input[type=email][name=Email]").val(), password: $("input[type=password][name=Password]").val() },
            type: 'GET',
            async: true,
            context: jQuery('#resultado')
        }).success(function(response){
                if (response.Message) {
                    ShowWarning(response.Message);
                    document.getElementById("btnlogin").disabled = false;
                    $(".pn-login-span-loader").hide();
                }
                else {
                    doLogin();
                }

        }).error(function(response){
                ShowDanger("Erro ao validar o login. Mensagem: "+response.Message);
                document.getElementById("btnlogin").disabled = false;
                $(".pn-login-span-loader").hide();                
        });     
    });

    function doLogin(){
    $.ajax({
            url: "/Login/Autenticar",
            data: {
                email: $("input[type=email][name=Email]").val(),
                senha: $("input[type=password][name=Password]").val(),
                remember: $("input[type=checkbox][name=Remember]").val()
            },
            type: "POST",
            context: jQuery('#resultado')
    }).success(function (response) {
        if (!response.Message) window.location.href = "Home/Index"
        else {
            ShowDanger("Erro ao fazer login: " + response.Message);
            document.getElementById("btnlogin").disabled = false;
            $(".pn-login-span-loader").hide();
        }
        
    }).error(function(response){
                    ShowDanger(response.Message);
                    document.getElementById("btnlogin").disabled = false;
                    $(".pn-login-span-loader").hide();
    });
}


});




