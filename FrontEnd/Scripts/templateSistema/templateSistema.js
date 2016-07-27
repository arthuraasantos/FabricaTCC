var pontoApp = angular.module('pontoApp',['angularModalService']);

pontoApp.controller('pontoController', function($scope, ModalService){

    $scope.templateImportacaoFuncionario = "importacaoFuncionarios.html";

    $scope.showDefaultModal = function(template){
            if(!template)
                template = 'template.html';
            ModalService.showModal({
                templateUrl: template,
                controller: "modalController"
            }).then(function(modal){
                modal.element.modal();
                modal.close.then(function(result){
                    $scope.message = "Voce escolheu" + result;
                });
            });
    };

   
});

pontoApp.controller('modalController', function($scope, close) {
  
 $scope.close = function(result) {
 	close(result, 500); // close, but give 500ms for bootstrap to animate
 };



});