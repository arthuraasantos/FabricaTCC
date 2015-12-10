function loadEmailLogged(){
	   $.ajax({
            url: "/Home/GetEmailEmployeeLogged",
            type: "GET",
            context: jQuery('#resultado'),
                
            error: function(response){
                    ShowDanger(response.Message);
            },
            success: function(response){

            }
    });
}