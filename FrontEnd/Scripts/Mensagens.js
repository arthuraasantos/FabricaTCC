function MostraMensagem(mensagem, titulo, icone, tipo, reload) {
    BootstrapDialog.show({
        title: titulo,
        closable: false,
        message: mensagem,
        type: BootstrapDialog.TYPE_PRIMARY,
        buttons: [{
            icon: icone,
            label: 'Fechar',
            cssClass: 'btn-primary',
            action: function(dialogRef){
                dialogRef.close();
                if (reload) { location.reload(); }
            }
        }]
    });
}



function MostraMensagem(mensagem, reload)
{
    BootstrapDialog.show({
        title: 'Mensagem do sistema',
        closable: false,
        message: mensagem,
        type: BootstrapDialog.TYPE_PRIMARY,
        buttons: [{
            label: 'Fechar',
            cssClass: 'btn-primary',
            action: function (dialogRef) {
                dialogRef.close();
                if (reload) { location.reload(); }
            }
        }]
    });
}
               
function MostraWarning(mensagem, reload) {
    BootstrapDialog.show({
        title: 'Atenção!',
        closable: false,
        message: mensagem,
        type: BootstrapDialog.TYPE_WARNING,
        buttons: [{
            label: 'Fechar',
            cssClass: 'btn-warning',
            action: function (dialogRef) {
                dialogRef.close();
                if (reload) { location.reload(); }
            }
        }]
    });
}

function MostraSuccess(mensagem, reload) {
    BootstrapDialog.show({
        title: 'Sucesso!',
        closable: false,
        message: mensagem,
        type: BootstrapDialog.TYPE_SUCCESS,
        buttons: [{
            label: 'Fechar',
            cssClass: 'btn-success',
            action: function (dialogRef) {
                dialogRef.close();
                if (reload) { location.reload(); }
            }
        }]
    });
}

function MostraDanger(mensagem, reload) {
    BootstrapDialog.show({
        title: 'Erro!',
        closable: false,
        message: mensagem,
        type: BootstrapDialog.TYPE_DANGER,
        buttons: [{
            label: 'Fechar',
            cssClass: 'btn-danger',
            action: function (dialogRef) {
                dialogRef.close();
                if (reload) { location.reload(); }
            }
        }]
    });
}