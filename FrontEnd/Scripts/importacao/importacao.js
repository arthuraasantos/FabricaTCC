pontoApp.controller('importacaoController', function($scope, $rootScope, $http, importacaoService){
    
    //Variáveis
    $scope.linhaBaseParaRelacionar = [];
    $scope.colunasImportacaoFuncionario = ["Bairro","Cep","Cpf","Cidade","Email","Estado","Logradouro","Nascimento","Nome","Número","Senha"];
    $scope.MensagemInfo = "";
    $scope.MensagemErro = "";
    $scope.textoPlanilha = "";

    // Funções
    $scope.prepararPlanilha = function(texto){
        var _planilha = [];

        var _textoFormatado = texto.trim().replace("\r","");

        //var _textoSplitado = _textoFormatado.split('\n')[0].split('\t');
        var _textoSplitado = _textoFormatado.split('\n');

        // for(var i=0; i < _textoSplitado.length; i++)
        // {
        //     _planilha += _textoFormatado.split('\n')[i].split('\t');
        // }

        $scope.textoPlanilha = _textoSplitado;
    };

    $scope.processarPrimeiraLinha = function(texto){

        var _textoPlanilha = texto.trim().replace("\r","");
        $scope.textoPlanilha = _textoPlanilha;
        
        var splitado = _textoPlanilha.split('\n')[0].split('\t');
        for(var i=0; i < splitado.length; i++)
        {
            var item = splitado[i];
            if(item.length >= 20){
                var textoCortado = splitado[i].substring(0,25);
                splitado[i] = textoCortado + '...';
            }
        };
        
        $scope.linhaBaseParaRelacionar = splitado;
    };

    $scope.validarPlanilha = function(ordemDasColunas){
        $scope.MensagemErro = "";
        $scope.MensagemInfo = "";
        var valoresUnicos = [];

        for(var i =0 ; i <$scope.linhaBaseParaRelacionar.length; i++)
        {
            if(!valoresUnicos.indexOf(ordemDasColunas[i]))
            {
                $scope.MensagemErro = "A mesma coluna foi marcada duas vezes";
                return;
            }
            else{
                valoresUnicos.push(ordemDasColunas[i]);
            }
        };

        if(valoresUnicos.indexOf("Senha") == -1)
        { 
            $scope.MensagemInfo = "A coluna 'Senha' é obrigatório para importar funcionários";
            return;
        }

        if(valoresUnicos.indexOf("Email") == -1)
        { 
            $scope.MensagemInfo = "A coluna 'Email' é obrigatório para importar funcionários";
            return;
        }

        $scope.prepararPlanilha($scope.textoPlanilha);

        importacaoService.importarFuncionarios($scope.textoPlanilha, ordemDasColunas)
        .success(function(data){
            $scope.MensagemInfo = data;
        }).error(function(data,status){
            $scope.MensagemErro = "Erro ao importar funcionarios:"+status;
        });

        
    };
});

