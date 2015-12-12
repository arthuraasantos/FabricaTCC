
function loadServices(){
	// Iniciar loads "carregando..."
	showLoaders();

	loadEmailLogged();
	loadAccessProfileLogged();
	loadNameEmployeeLogged();
    loadOrganizationNameEmployeeLogged();
    loadEmployeeLoggedWorkHour();
    loadEmployeeHitHourForDay();
    loadEmployeeNotificationWarning();
    loadCountPendingSolicitation();
    loadCountPendingClearance();
    loadCountPendingVacation();
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
			    $(".pn-index-employee-name").text(response.Message);
			    $(".pn-index-employee-name-loader").hide();
			}).error(function(response){
				ShowDanger(response.Message);
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
			});
}

function loadEmployeeHitHourForDay(){
	   $.ajax({
            url: "/Home/GetHitHourForDay",
            type: "GET",
            context: jQuery('#resultado'),
            async: true
			}).success(function(response){
			    $(".pn-index-employee-hithour").text(response.Message);
			    $(".pn-index-employee-hithour-loader").hide();

			    if (response.Message){
					document.getElementById("employee-hithour").style.display = 'block';
			    };
			}).error(function(response){
				ShowDanger(response.Message);
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
			});
}

function loadCountPendingSolicitation(){
   $.ajax({
            url: "/Home/GetCountPendingHours",
            type: "GET",
            context: jQuery('#resultado'),
            async: true
			}).success(function(response){
			    $(".pn-index-solicitation-hour-pending").text(response.Message);
			    $(".pn-index-solicitation-hour-pending-loader").hide();
			}).error(function(response){
				ShowDanger(response.Messages);
			});	
}

function loadCountPendingClearance(){
   $.ajax({
            url: "/Home/GetCountPendingClearance",
            type: "GET",
            context: jQuery('#resultado'),
            async: true
			}).success(function(response){
			    $(".pn-index-solicitation-clearance-pending").text(response.Message);
			    $(".pn-index-solicitation-clearance-pending-loader").hide();
			}).error(function(response){
				ShowDanger(response.Messages);
			});	
}

function loadCountPendingVacation(){
   $.ajax({
            url: "/Home/GetCountPendingVacation",
            type: "GET",
            context: jQuery('#resultado'),
            async: true
			}).success(function(response){
			    $(".pn-index-solicitation-vacation-pending").text(response.Message);
			    $(".pn-index-solicitation-vacation-pending-loader").hide();
			}).error(function(response){
				ShowDanger(response.Messages);
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
	$(".pn-index-solicitation-hour-pending-loader").show();						    // Loading da quantidade de horas pendentes    
	$(".pn-index-solicitation-clearance-pending-loader").show();					// Loading da quantidade de folgas pendentes    
	$(".pn-index-solicitation-vacation-pending-loader").show();					    // Loading da quantidade de férias pendentes    

}

