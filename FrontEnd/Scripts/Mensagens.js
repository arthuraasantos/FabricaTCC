function MostraMensagem(mensagem, titulo, icone, tipo) {
    BootstrapDialog.show({
        title: titulo,
        message: mensagem,
        type: BootstrapDialog.TYPE_INFO,
        buttons: [{
            icon: icone,
            label: 'Fechar',
            cssClass: 'btn-info',
            action: function(dialogRef){
                dialogRef.close();
                location.reload();
            }
        }]
    });
}