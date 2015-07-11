﻿
var App_DefaultView = _package('App.DefaultView');

App_DefaultView.addPanelUnidades = function () {
    var negocioClient = Client_NegocioClient;
    var builder = Builder_DefaultBuilder;
    var panel = $('#panelUnidades');
    var part;

    negocioClient.getUnidadesActivas(function () {
        $.each(negocioClient.unidadesNegocio, function (_i, _unidadNegocio) {
            part = builder.getUnidadPanel(_unidadNegocio.IdUnidadNegocio);

            panel.append(part);
        });
    }, null);
};

App_DefaultView.addTurnoNegocio = function (_idUnidadNegocio, _idTurno, _puntoAtencion, _nombreEmpleado) {
    var builder = Builder_DefaultBuilder;
    var mensaje = builder.getMensajeAtencion(_nombreEmpleado, _puntoAtencion);
    var atendiendo = builder.getMensajeAtencion1(_idUnidadNegocio);
    var image = _get('imageUnidadDefault');
    var color = _get('colorUnidadDefault');
    var color1 = _get('colorUnidadDefault');
    var idTurno = _get('negocioNumberDefault');

    if (!_isUndefined(_idUnidadNegocio)) {
        unidadNegocio = Client_NegocioClient.unidadesNegocio[_idUnidadNegocio];

        if (!_isUndefined(unidadNegocio)) {
            image = builder.getImageUnidad(_idUnidadNegocio);
            color = unidadNegocio.Color;

            idTurno = (!_isUndefined(_idTurno) ? _leftPad(_idTurno, _get('padNumberDefault')) : idTurno);
            color1 = '#FFFFFF';
        }
    }

    $('#mensajeAtencion').replaceWith(mensaje);
    $('#lblAtendiendo').text(atendiendo);

    $('#negocioImage').attr('src', image);
    $('#turnoNumber').text(idTurno);

    $('.datosDetail').css('color', '#FFFFFF');
    $('.turnoNumber').css('color', '#FFFFFF');
    $('#izquierda').css('background-color', color);
};

App_DefaultView.addTurnoUnidad = function (_idUnidadNegocio, _idTurno) {
    if (!_isUndefined(_idTurno) && _idTurno > 0) {
        $('#turnoUnidad-' + _idUnidadNegocio).text(_leftPad(_idTurno, _get('padNumberDefault')));
    } else {
        $('#turnoUnidad-' + _idUnidadNegocio).text(_get('unidadNumberDefault'));
    }
};

App_DefaultView.addEmpleado = function (_empleado) {
    var builder = Builder_DefaultBuilder;
    var foto = _get('imageEmpleadoDefault');
    var nombre = '';

    if (!_isUndefined(_empleado) && !_isNull(_empleado)) {
        foto = (!_isUndefined(_empleado.NoEmpleado) && !_isNull(_empleado.NoEmpleado)
            ? builder.getImageEmpleado(_empleado.NoEmpleado) : foto);
        nombre = (!_isUndefined(_empleado.Nombre) && !_isNull(_empleado.Nombre)
            ? _empleado.Nombre : nombre);
    }

    $('#empleadoImage').attr('src', foto);
    $('#empleadoNombre').text(nombre);
};


