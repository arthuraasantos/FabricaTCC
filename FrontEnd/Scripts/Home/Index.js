
function loadAdminServices(){
	// Iniciar loads "carregando..."
	showLoaders();
	// Buscar email de funcionário logado
	loadEmailLogged();
	loadAccessProfileLogged();
	loadNameEmployeeLogged();
    loadOrganizationNameEmployeeLogged();
}

function loadEmailLogged(){
	   $.ajax({
            url: "/Home/GetEmailEmployee",
            type: "GET",
            context: jQuery('#resultado'),
            async: true
			}).success(function(response){
			    $(".pn-index-employee-email").text(response.Message);

			    $(".pn-index-employee-email-loader").hide();
			}).error(function(response){
				ShowDanger(response.Message);
				// parar load
			});
}

function loadAccessProfileLogged(){
	   $.ajax({
            url: "/Home/GetAccessProfileDescription",
            type: "GET",
            context: jQuery('#resultado'),
            async: true
			}).success(function(response){
				// altera label
			    $(".pn-index-employee-profile-description").text(response.Message);
			    $(".pn-index-employee-profile-description-loader").hide();
			}).error(function(response){
				ShowDanger(response.Message);
				// parar load
			});
}

function loadNameEmployeeLogged(){
	   $.ajax({
            url: "/Home/GetNameEmployee",
            type: "GET",
            context: jQuery('#resultado'),
            async: true
			}).success(function(response){
				// altera label
			    $(".pn-index-employee-name").text(response.Message);
			    $(".pn-index-employee-name-loader").hide();
			}).error(function(response){
				ShowDanger(response.Message);
				// parar load
			});
}

function loadOrganizationNameEmployeeLogged(){
	   $.ajax({
            url: "/Home/GetOrganizationName",
            type: "GET",
            context: jQuery('#resultado'),
            async: true
			}).success(function(response){
			    $(".pn-index-employee-organization").text(response.Message);
			    $(".pn-index-employee-organization-loader").hide();
			}).error(function(response){
				ShowDanger(response.Message);
				// parar load
			});
}



function showLoaders(){
	$(".pn-index-employee-email-loader").show();									// Loading do email do funcionário logado
	$(".pn-index-employee-profile-description-loader").show();						// Loading do perfil de descrição do funcionário logado
	$(".pn-index-employee-name-loader").show();						                // Loading do nome do funcionário logado
    $(".pn-index-employee-organization-loader").show();						        // Loading do nome da empresa do funcionário logado
}
