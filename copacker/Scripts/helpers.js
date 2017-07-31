//
// Llamada sincrónica.
//

jQuery.extend
(
    {
        getValues: function (url) {
            var result = null;
            $.ajax(
                {
                    url: url,
                    type: 'get',
                    dataType: 'html',
                    async: false,
                    cache: false,
                    success: function (data) {
                        result = data;
                    }
                }
            );
            return result;
        }
    }
);

var JSONResponseType_OkOnly = 0,
    JSONResponseType_ErrorOnly = 1,
    JSONResponseType_Message = 2,
    JSONResponseType_RedirectToPage = 3,
    JSONResponseType_Popup = 4,
    JSONResponseType_CustomText = 5;


var JSONRequestOptions = {
    showProgress: true,
    async: true
};

jQuery.extend
(
    {
        JSONRequest: function (_url, _data, _options) {

            var result;

            // Hace un merge de los parametros por default y los que recibe por parametro
            var settings = Object.assign({}, JSONRequestOptions, _options);

            $.ajax(
                {
                    url: _url,
                    data: JSON.stringify(_data),
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    async: settings.async,
                    cache: false,
                    beforeSend: function () {
                        if (settings.showProgress) {
                            // Mostramos el progress bar
                            progress.show();
                        }
                    },
                    success: function (data) {

                        if (settings.showProgress) {
                            // Ocultamos el progress bar
                            progress.hide();
                        }

                        if (data == null || data.type == undefined || data.type == null) {
                            console.log('JSONRequest: (Error resolve with library) ' + _url + ' | ' + (_data != null ? JSON.stringify(_data) : ' NO DATA '));
                            return false;
                        }

                        result = data;

                        // Verificamos si viene MENSAJE
                        if (data.type == JSONResponseType_OkOnly) {
                            console.log('JSONResponseType_OkOnly');
                        }
                        if (data.type == JSONResponseType_ErrorOnly) {
                            console.log('JSONResponseType_ErrorOnly');
                        } else if (data.type == JSONResponseType_Message) {
                            console.log('JSONResponseType_Message');
                            $.messageTypeResolve(data.content);
                        } else if (data.type == JSONResponseType_RedirectToPage) {

                        } else if (data.type == JSONResponseType_Popup) {

                        } else if (data.type == JSONResponseType_CustomText) {
                            //Verificamos que se encuentre definida la función de callback
                            if (result.content != undefined && result.content != null &&
                                result.content.fnCallBack != undefined && result.content.fnCallBack != null && result.content.fnCallBack != '') {

                                var _fnCallback = eval(result.content.fnCallBack);
                                _fnCallback(result); // Invocamos a la función definida
                            }
                        }


                        return result;
                    }
                }
            );
            return result;
        }
    }
);

jQuery.extend
(
    {
        fnFormatting: function (fnStr) {

            if (fnStr.indexOf("))") == -1) { // Verifico que notenga un doble cierre. Ya que si es así es evaluado como función y como cierre de parámetros.
                if (fnStr.indexOf(")") == -1) { // Si NO tiene ni siquiera un cierre, tengo que agregarle la apertura mas los 2 cierres.
                    fnStr = "(" + v + "())";
                } else {
                    fnStr = "(" + fnStr + ")"; // Solo el cierre para que sea evaluado con función.
                }


            }

            return fnStr;
        }
    }
);


jQuery.extend
(
    {
        messageTypeResolve: function (_content) {

            var boxMsj;
            var result = true;
            var callerOkButton = null;


            if (_content.fnButton1 != undefined && _content.fnButton1 != null) {
                callerOkButton = function () {
                    result = eval($.fnFormatting(data.fnButton1));

                    // Verificamos si devuelbe un true para proceder a cerrar el bootbox.;
                    if (result) {
                        boxMsj.modal('hide');
                    }

                };
            } else {

                // Si es que no se definió fnButton1 que es la llamada a una función local. la function para cerrar el popup directamente.
                callerOkButton = function () {
                    boxMsj.modal('hide');
                };
            }

            boxMsj = msj.alerta(_content.title, _content.mensaje, callerOkButton);

            return;
        }
    }
);

jQuery.extend
(
    {
        redirect: function (_url) {

            progress.show();
            window.location.href = _url;

        }
    }
);


//
// Ventana de progreso
//

var progress = progress || (function ($) {
    'use strict';

    // Creating modal dialog's DOM
    var $dialog = $(
        '<div class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:15%; overflow-y:visible; z-index: 9999;">' +
        '<div class="modal-dialog modal-m" style="width: 200px; heigth: 200px;">' +
        '<div class="modal-content" style="height: 100px;">' +
            '<div class="modal-header"><h3 style="margin:0;"></h3></div>' +
            '<div class="modal-body">' +
                '<div class="progress progress-striped active" style="margin-bottom:0; margin-top: -5px !important"><div class="progress-bar" style="width: 100%"></div></div>' +
            '</div>' +
        '</div></div></div>');

    return {
        inits: 0,
        show: function (message, options) {
            console.log("show - before / inits: " + this.inits);
            this.inits++;

            // Assigning defaults
            if (typeof options === 'undefined') {
                options = {};
            }
            if (typeof message === 'undefined') {
                message = 'Procesando...';
            }
            var settings = $.extend({
                dialogSize: 'm',
                progressType: '',
                onHide: null // This callback runs after the dialog was hidden
            }, options);

            // Configuring dialog
            $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
            $dialog.find('.progress-bar').attr('class', 'progress-bar');
            if (settings.progressType) {
                $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
            }
            $dialog.find('h3').text(message);
            // Adding callbacks
            if (typeof settings.onHide === 'function') {
                $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                    settings.onHide.call($dialog);
                });
            }
            // Opening dialog
            $dialog.modal();
        },
        /**
            * Closes dialog
            */
        hide: function () {

            //console.log("hide - before / inits: " + this.inits);

            this.inits = this.inits == 0 ? this.inits : this.inits-1;
            if (this.inits <= 0) { $dialog.modal('hide'); console.log("HIDE");}

            //console.log("hide - after / inits: " + this.inits);

        }
    };

})(jQuery);





//
// Validador numérico.
//

// Con esta funcion solo permite ingresar valores numericos.
function onlyNumber(e) {

    // Allow: backspace, delete, tab, escape, enter and .
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
        // Allow: Ctrl+A, Command+A
        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: home, end, left, right, down, up
        (e.keyCode >= 35 && e.keyCode <= 40)) {
        // let it happen, don't do anything
        return;
    }

    ////////// Esto es para habilitar el copy & paste
    //////////
    var ctrlDown = false,
         ctrlKey = 17,
         cmdKey = 91,
         vKey = 86,
         cKey = 67;

    if ((e.ctrlKey) && (e.keyCode == vKey || e.keyCode == cKey)) {
        return;
    }


    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
}


function goUrl(_url) {

    progress.show();
    window.location.href = _url;
}
    

$(document).ready(function() {
    $('.numeric').keydown(onlyNumber);
});

function RenderizarDetalle() {
    if (_options.success != undefined && _options.success != null) {
        _options.success(result);
    }
}