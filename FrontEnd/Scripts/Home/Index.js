﻿
function loadServices(){
	// Iniciar loads "carregando..."
	showLoaders();

	// Buscar email de funcionário logado
	loadEmailLogged();
	loadAccessProfileLogged();
	loadNameEmployeeLogged();
    loadOrganizationNameEmployeeLogged();
    loadEmployeeLoggedWorkHour();
    loadEmployeeHitHourForDay();
    loadEmployeeNotificationWarning();
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
	if ($(".pn-index-employee-profile-description").text() == "") 
	{
	   $.ajax({
            url: "/Home/GetAccessProfileDescription",
            type: "GET",
            context: jQuery('#resultado'),
            async: true
			}).success(function(response){
			    $(".pn-index-employee-profile-description").text(response.Message);
			    $(".pn-index-employee-profile-description-loader").hide();

			    if(response.Message == "Administrador")
					$(".pn-index-adm").show();			    		
				else
					$(".pn-index-no-adm").show();			    		

			}).error(function(response){
				ShowDanger(response.Message);
			});
	};
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

function loadEmployeeLoggedWorkHour(){
	   $.ajax({
            url: "/Home/GetEmployeeWorkHour",
            type: "GET",
            context: jQuery('#resultado'),
            async: true
			}).success(function(response){
			    $(".pn-index-employee-workhours").text(response.Message);
			    $(".pn-index-employee-workhours-loader").hide();
			}).error(function(response){
				ShowDanger(response.Message);
				// parar load
			});
}

function loadEmployeeHitHourForDay(){
	   $.ajax({
            url: "/Home/GetHitHourForDay",
            type: "GET",
            context: jQuery('#resultado'),
            async: true
			}).success(function(response){
				//response.Message = "chegou 1";
			    $(".pn-index-employee-hithour").text(response.Message);
			    $(".pn-index-employee-hithour-loader").hide();

			    if (response.Message){
					document.getElementById("employee-hithour").style.display = 'block';
			    };
			}).error(function(response){
				ShowDanger(response.Message);
				// parar load
			});
}

function loadEmployeeNotificationWarning(){
	   $.ajax({
            url: "/Home/GetNotificationWarning",
            type: "GET",
            context: jQuery('#resultado'),
            async: true
			}).success(function(response){
			    $(".pn-index-employee-notification-warning").text(response.Message);
			    $(".pn-index-employee-notification-warning-loader").hide();

			     if (response.Message){
					document.getElementById("employee-notification-warning").style.display = 'block';
			    };
			}).error(function(response){
				ShowDanger(response.Messages);
				// parar load
			});
}


function showLoaders(){
	$(".pn-index-employee-email-loader").show();									// Loading do email do funcionário logado
	$(".pn-index-employee-profile-description-loader").show();						// Loading do perfil de descrição do funcionário logado
	$(".pn-index-employee-name-loader").show();						                // Loading do nome do funcionário logado
    $(".pn-index-employee-organization-loader").show();						        // Loading do nome da empresa do funcionário logado
    $(".pn-index-employee-workhours-loader").show();						        // Loading da quantidade de horas trabalhadas do funcionário logado
    $(".pn-index-employee-hithour-loader").show();						       		// Loading da quantidade batidas do funcionário logado no dia
	$(".pn-index-employee-notification-warning-loader").show();						// Loading da avisos para o funcionário    


}

