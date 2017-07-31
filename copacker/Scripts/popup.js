function PopupValidacion(validaciones)
{

    bootbox.dialog({
        message: validaciones,
        title: "Datos incorrectos o faltantes",
        buttons: {
            success: {
                label: "Cerrar",
                className: "btn-success"
            }
        }
    });


}