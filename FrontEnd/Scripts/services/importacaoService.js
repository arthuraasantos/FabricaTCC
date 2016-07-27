angular.module("pontoApp").factory("importacaoService", function($http){
    var _importarFuncionarios = function(textoPlanilha, relacaoColunas){
        var data = {
            "textoPlanilha" : textoPlanilha,
            "relacaoColunas" : relacaoColunas
        };

        return $http.post("/Importacao/ImportarFuncionarios",data);
    };
    
    return {
        importarFuncionarios : _importarFuncionarios
    };
});