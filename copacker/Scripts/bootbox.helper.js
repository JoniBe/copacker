var msj = {};

$(document).ready(function () {

    // Configuramos todos los cierres que el backdrop sea estático.

    msj.confirmar = function (title, mensaje, cName, fnCancel, fnOk) {

        return bootbox.dialog({
            closeButton: false,
            message: mensaje,
            title: title,
            className: cName,
            buttons: {
                "Aceptar": {
                    label: "Aceptar",
                    className: "btn-primary",
                    callback: function () {
                        fnOk();
                        $('.modal-backdrop').remove();
                        return false;
                    }
                },
                cancel: {
                    label: "Cancelar",
                    className: "btn-default",
                    callback: fnCancel
                }
            }
        });
    };


    msj.alerta = function (title, mensaje, fnOk) {

        return bootbox.dialog({
            closeButton: false,
            message: mensaje,
            title: title,
            buttons: {
                "Aceptar": {
                    label: "Aceptar",
                    className: "btn-primary",
                    callback: function () {

                        if (fnOk != null) {
                            fnOk();
                        } else {
                            $(this).modal('hide');
                        }

                        $('.modal-backdrop').remove();
                        return false;
                    }
                }

            }
        });
    };

    msj.alertaBig = function (title, mensaje, fnOk) {

        return bootbox.dialog({
            closeButton: false,
            message: mensaje,
            title: title,
            className: 'bigModal',
            buttons: {
                "Aceptar": {
                    label: "Aceptar",
                    className: "btn-primary",
                    callback: function () {
                        fnOk();
                        $('.modal-backdrop').remove();
                        return false;
                    }
                }

            }
        });
    };


    msj.mostrar = function (url, title, cName, fnCancel, fnOk) {

        progress.show();

        var htmlString = $.getValues(url);

        progress.hide();

        return msj.confirmar(title, htmlString, cName, fnCancel, fnOk);

    };


});