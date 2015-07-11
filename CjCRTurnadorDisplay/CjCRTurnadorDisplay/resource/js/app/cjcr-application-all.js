
// -------------------------------- data/cjcr-application-data.js [BEGIN]

var APPLICATION_DATA = {
    "production": true,

    "turnoClientTaskTime": 3000,

    "defaultAuthorization": "{\"dt\": true}",

    "negocioServiceBasePro": "http://nt0055/BazCajaFront/Servicios/ServiciosTurnador/CjCRNegocioService.svc/",
    "turnosServiceBasePro": "http://nt0055/BazCajaFront/Servicios/ServiciosTurnador/CjCRTurnosService.svc/",
    "negocioServiceBaseDev": "http://localhost:3927/CjCRNegocioService.svc/",
    "turnosServiceBaseDev": "http://localhost:3927/CjCRTurnosService.svc/",

    "colorUnidadDefault": "#565656",
    "unidadNumberDefault": "000",
    "negocioNumberDefault": "000",
    "padNumberDefault": 3,

    "imageEmpleadoDefault": "resource/img/hdtv-picframe.png",
    "imageEmpleadoPart": "resource/img/empleados/empleado-foto-{noEmpleado}.jpg",
    "imageUnidadDefault": "resource/img/hdtv-negframe.png",
    "imageUnidadPart": "resource/img/negocio/unidad-logo-{idUnidadNegocio}.png",
    "imageTicket": "resource/img/hdtv-ticket.png",

    "beep": "resource/sound/beep.wav"
};

// -------------------------------- data/cjcr-application-data.js [END]


// -------------------------------- data/cjcr-messages-data.js [BEGIN]

var MESSAGES_DATA = {
    "mensajeAtencion1": "Bienvenido, tome un turno para ser atendido",
    "mensajeAtencion2": "Por favor, pase a la zona de",
    "mensajeAtencion3": "Por favor, pase a que lo atienda:",
    "turnoSolicitado": "Turno solicitado - negocio: {descripcion}, turno: {idTurno}"
};

// -------------------------------- data/cjcr-messages-data.js [END]


// -------------------------------- support/cjcr-turnador-panel-status.js [BEGIN]

var _OPERATION_COMPLETE = 0;
var _OPERATION_FAIL = -1;

// -------------------------------- support/cjcr-turnador-panel-status.js [END]


// -------------------------------- support/cjcr-asserts.js [BEGIN]

function _isUndefined(_value) {
    if (typeof _value === 'undefined') {
        return true;
    } else {
        return false;
    }
}

function _isNull(_value) {
    if (_value === null) {
        return true;
    } else {
        return false;
    }
}

function _assertUndefined(_value, _message) {
    if (typeof _value === 'undefined') {
        if (typeof _message === 'undefined') {
            _fail('Value is undefined');
        } else {
            _fail(_message);
        }
    }
}

// -------------------------------- support/cjcr-asserts.js [END]


// -------------------------------- support/cjcr-app-core.js [BEGIN]

var _PACKAGE_BASE = {};

var App_Data = _package('App.Data');

App_Data.appConfig = {};
App_Data.messages = {};

function _package(namespace) {
    var nsparts = namespace.split('.');
    var parent = _PACKAGE_BASE;

    for (var i = 0; i < nsparts.length; i++) {
        var partname = nsparts[i];

        if (typeof parent[partname] === 'undefined') {
            parent[partname] = {};
        }

        parent = parent[partname];
    }

    return parent;
}

function _fail(_message) {
    throw new Error(_message);
}

function _get(_name) {
    var appConfig = App_Data.appConfig;
    var value = appConfig[_name];

    if (_isUndefined(value)) {
        if (appConfig.production) {
            value = appConfig[_name + 'Pro'];
        } else {
            value = appConfig[_name + 'Dev'];
        }
    }

    return value;
}

function _getMessage(_name) {
    var messages = App_Data.messages;

    return messages[_name];
}

function _beep() {
    var beep = '<embed id="beep" src="{source}" autostart="true" loop="false" hidden="true" type="audio/wav" >';

    beep = beep.replace('{source}', _get('beep'));

    $('#beep').remove();
    $('#beepPanel').html(beep);
}

function _leftPad(_value, _pad) {
    var value = new String(_value);
    var zero = '0';

    for (var i = value.length; i < _pad; i++) {
        value = zero.concat(value);
    }

    return value;
}

function _console(_message, _alert) {
    if (!$('html').is('.ie6, .ie7')) {
        console.log(_message);
    }

    if (!_get('production') && !_isUndefined(_alert) && _alert) {
        alert(_message);
    }
}

// -------------------------------- support/cjcr-app-core.js [END]


// -------------------------------- support/cjcr-app-config.js [BEGIN]

function _init(_callback) {
    var appData = App_Data;

    appData.appConfig = APPLICATION_DATA;
    appData.messages = MESSAGES_DATA;

    _callback();
}

// -------------------------------- support/cjcr-app-config.js [END]


// -------------------------------- builder/cjcr-default-builder.js [BEGIN]

var Builder_DefaultBuilder = _package('Builder.DefaultBuilder');

Builder_DefaultBuilder.getUnidadPanel = function (_idUnidadNegocio) {
    var _this = Builder_DefaultBuilder;
    var part = '<div class="span2b8">';

    part = part.concat('<img src="');
    part = part.concat(_get('imageTicket'));
    part = part.concat('" class="img-rounded unidadImage"/>');
    part = part.concat('<img src="');
    part = part.concat(_this.getImageUnidad(_idUnidadNegocio));
    part = part.concat('" class="img-rounded logoUnidadImage"/>');
    part = part.concat('<p id="turnoUnidad-');
    part = part.concat(_idUnidadNegocio);
    part = part.concat('" class="turnoUnidad">');
    part = part.concat(_get('unidadNumberDefault'));
    part = part.concat('</p></div>');

    return part;
};

Builder_DefaultBuilder.getMensajeAtencion = function (_idUnidadNegocio) {
    var mensaje = _getMessage('mensajeAtencion1');
    var part = '<p id="mensajeAtencion" class="mensajeDetail">';

    if (!_isUndefined(_idUnidadNegocio)) {
        unidadNegocio = Client_NegocioClient.unidadesNegocio[_idUnidadNegocio];

        if (!_isUndefined(unidadNegocio)) {
            mensaje = _getMessage('mensajeAtencion2');

            mensaje = mensaje.concat('<br><span class="datosDetail">');
            mensaje = mensaje.concat(unidadNegocio.Descripcion.toUpperCase());
            mensaje = mensaje.concat('</span> ');
        }
    }

    part = part.concat(mensaje);
    part = part.concat('</p>');

    return part;
};

Builder_DefaultBuilder.getImageUnidad = function (_idUnidadNegocio) {
    return _get('imageUnidadPart').replace(/\{idUnidadNegocio\}/g, _idUnidadNegocio);
};

Builder_DefaultBuilder.getImageEmpleado = function (_noEmpleado) {
    return _get('imageEmpleadoPart').replace(/\{noEmpleado\}/g, _noEmpleado);
};

// -------------------------------- builder/cjcr-default-builder.js [END]


// -------------------------------- client/cjcr-negocio-client.js [BEGIN]

var Client_NegocioClient = _package('Client.NegocioClient');

Client_NegocioClient.unidadesNegocio = {};
Client_NegocioClient.unidadesActivas = [];

Client_NegocioClient.GET_UNIDADES_ACTIVAS = 0;

Client_NegocioClient.getUnidadesActivas = function (_callback, _callbackError) {
    var _this = Client_NegocioClient;

    _this.callService(_this.GET_UNIDADES_ACTIVAS, _callback, _callbackError);
};

Client_NegocioClient.getUnidadesActivasSuccess = function (_data, _status, _jqXHR, _callback) {
    var _this = Client_NegocioClient;

    _this.unidadesNegocio = {};
    _this.unidadesActivas = [];

    _assertUndefined(_callback, 'Callback function is undefined');

    $.each(_data, function (_i, _unidadNegocio) {
        _this.unidadesNegocio[_unidadNegocio.IdUnidadNegocio] = _unidadNegocio;
        _this.unidadesActivas[_i] = _unidadNegocio;
    });

    _callback(_data);
};

Client_NegocioClient.callService = function (_service, _callback, _callbackError, _data, _paths) {
    var _this = Client_NegocioClient;

    _data = (!_isUndefined(_data) && !_isNull(_data) ? _data : '');

    $.ajax(_this.getUrl(_service, _paths), {
        type: _this.getType(_service),
        data: JSON.stringify(_data),
        dataType: 'json',
        success: function (_data, _status, _jqXHR) {
            _this.callServiceSuccess(_data, _status, _jqXHR, _service, _callback);
        },
        error: function (_jqXHR, _status, _error) {
            _this.callServiceError(_jqXHR, _status, _error, _callbackError, _service);
        }
    });
};

Client_NegocioClient.callServiceSuccess = function (_data, _status, _jqXHR, _service, _callback) {
    var _this = Client_NegocioClient;

    switch (_service) {
        case _this.GET_UNIDADES_ACTIVAS:
            return _this.getUnidadesActivasSuccess(_data, _status, _jqXHR, _callback);
        default:
            _fail('Invalid service');
    }
};

Client_NegocioClient.callServiceError = function (_jqXHR, _status, _error,
		_callbackError, _service) {
    var message = 'service: {service}, status: {status}, error: {error}';

    message = message.replace(/\{error\}/g, JSON.stringify(_error));
    message = message.replace(/\{service\}/g, _service);
    message = message.replace(/\{status\}/g, _status);

    _console(message, true);

    if (!_isUndefined(_callbackError) && !_isNull(_callbackError)) {
        _callbackError();
    }
};

Client_NegocioClient.getType = function (_service) {
    var _this = Client_NegocioClient;

    switch (_service) {
        case _this.GET_UNIDADES_ACTIVAS:
            return 'GET';
        default:
            _fail('Invalid service');
    }
};

Client_NegocioClient.getUrl = function (_service, _paths) {
    var _this = Client_NegocioClient;
    var url = _get('negocioServiceBase');

    switch (_service) {
        case _this.GET_UNIDADES_ACTIVAS:
            url = url.concat('negocio/');
            break;
        default:
            _fail('Invalid service');
    }

    return url;
};

// -------------------------------- client/cjcr-negocio-client.js [END]


// -------------------------------- client/cjcr-turnos-client.js [BEGIN]

var Client_TurnosClient = _package('Client.TurnosClient');

Client_TurnosClient.unidadesNegocio = {};

Client_TurnosClient.TURNOS_ASIGNADOS = 3;

Client_TurnosClient.getTurnosAsignados = function (_callback, _callbackError) {
    var _this = Client_TurnosClient;
    var fecha = new Date();

    _this.callService(_this.TURNOS_ASIGNADOS, _callback, _callbackError, null, {
        fecha: fecha.format('yyyymmdd')
    });
};

Client_TurnosClient.getTurnosAsignadosSuccess = function (_data, _status, _jqXHR, _callback) {
    var _this = Client_TurnosClient;

    _this.unidadesNegocio = {};

    _assertUndefined(_callback, 'Callback function is undefined');

    $.each(_data, function (_i, _entry) {
        _this.unidadesNegocio[_entry.Key] = _entry.Value;
    });

    _callback(_data);
};

Client_TurnosClient.callService = function (_service, _callback, _callbackError, _data, _paths) {
    var _this = Client_TurnosClient;

    _data = (!_isUndefined(_data) && !_isNull(_data) ? _data : '');

    $.ajax(_this.getUrl(_service, _paths), {
        type: _this.getType(_service),
        data: JSON.stringify(_data),
        dataType: 'json',
        success: function (_data, _status, _jqXHR) {
            _this.callServiceSuccess(_data, _status, _jqXHR, _service, _callback);
        },
        error: function (_jqXHR, _status, _error) {
            _this.callServiceError(_jqXHR, _status, _error, _callbackError, _service);
        }
    });
};

Client_TurnosClient.callServiceSuccess = function (_data, _status, _jqXHR, _service, _callback) {
    var _this = Client_TurnosClient;

    switch (_service) {
        case _this.TURNOS_ASIGNADOS:
            return _this.getTurnosAsignadosSuccess(_data, _status, _jqXHR, _callback);
        default:
            _fail('Invalid service');
    }
};

Client_TurnosClient.callServiceError = function (_jqXHR, _status, _error,
		_callbackError, _service) {
    var message = 'service: {service}, status: {status}, error: {error}';

    message = message.replace(/\{error\}/g, JSON.stringify(_error));
    message = message.replace(/\{service\}/g, _service);
    message = message.replace(/\{status\}/g, _status);

    _console(message, true);

    if (!_isUndefined(_callbackError) && !_isNull(_callbackError)) {
        _callbackError();
    }
};

Client_TurnosClient.getType = function (_service) {
    var _this = Client_TurnosClient;

    switch (_service) {
        case _this.TURNOS_ASIGNADOS:
            return 'GET';
        default:
            _fail('Invalid service');
    }
};

Client_TurnosClient.getUrl = function (_service, _paths) {
    var _this = Client_TurnosClient;
    var url = _get('turnosServiceBase');

    switch (_service) {
        case _this.TURNOS_ASIGNADOS:
            url = url.concat('turno/{fecha}/negocio/asignados/');
            url = url.replace('{fecha}', _paths.fecha);
            break;
        default:
            _fail('Invalid service');
    }

    return url;
};

// -------------------------------- client/cjcr-turnos-client.js [END]


// -------------------------------- logic/cjcr-turnos-manager.js [BEGIN]

var Logic_TurnosManager = _package('Logic.TurnosManager');

Logic_TurnosManager.controlAsignados = {};
Logic_TurnosManager.controlHistorico = {};
Logic_TurnosManager.asignadoBuffer = null;
Logic_TurnosManager.historicoBuffer = {};
Logic_TurnosManager.refAsignado = false;
Logic_TurnosManager.refHistorico = {};

Logic_TurnosManager.init = function () {
    var _this = Logic_TurnosManager;
    var turnosClient = Client_TurnosClient;

    setInterval(function () {
        turnosClient.getTurnosAsignados(function () {
            _this.distributeTurnos(turnosClient.unidadesNegocio);
        }, null);
    }, _get('turnoClientTaskTime'));
};

Logic_TurnosManager.initControl = function () {
    var _this = Logic_TurnosManager;
    var negocioClient = Client_NegocioClient;
    var unidadNegocio;

    for (var i = 0; i < negocioClient.unidadesActivas.length; i++) {
        unidadNegocio = negocioClient.unidadesActivas[i];

        _this.historicoBuffer[unidadNegocio.IdUnidadNegocio] = null;
        _this.refHistorico[unidadNegocio.IdUnidadNegocio] = false;
    }
};

Logic_TurnosManager.distributeTurnos = function (_unidadesNegocio) {
    var _this = Logic_TurnosManager;
    var bAsignado;
    var bPrincipal;
    var turnos;

    _this.checkBuffer(_this.controlAsignados);

    $.each(_this.historicoBuffer, function (_idUnidadNegocio, _current) {
        turnos = _unidadesNegocio[_idUnidadNegocio];

        _this.checkBuffer(_this.controlHistorico[_idUnidadNegocio]);

        if (!_isUndefined(turnos)) {
            $.each(turnos, function (_i, _turno) {
                bPrincipal = (_i === 0);
                bAsignado = _this.setAsignado(_turno, bPrincipal);

                if (!bAsignado) {
                    _this.setHistorico(_idUnidadNegocio, _turno);
                }
            });
        }

        _this.cleanBuffer(_this.controlHistorico[_idUnidadNegocio]);
        _this.currentHistorico(_idUnidadNegocio);
        _this.updateHistorico(_idUnidadNegocio);
    });

    _this.cleanBuffer(_this.controlAsignados);
    _this.currentAsignado();
    _this.updateAsignado();
};

Logic_TurnosManager.setAsignado = function (_turno, _bPrincipal) {
    var _this = Logic_TurnosManager;
    var control = _this.controlAsignados[_turno.IdTurno];

    if (_isUndefined(control)) {
        control = {
            'select': _this.refAsignado,
            'turno': _turno,
            'solicitado': false,
            'reference': true,
            'historico': false
        };
    } else {
        control.reference = true;
    }

    if (!control.historico && (!control.solicitado || _bPrincipal)) {
        _this.controlAsignados[_turno.IdTurno] = control;

        return true;
    } else {
        return false;
    }
};

Logic_TurnosManager.setHistorico = function (_idUnidadNegocio, _turno) {
    var _this = Logic_TurnosManager;
    var historico = (_isUndefined(_this.controlHistorico[_idUnidadNegocio])
            ? {} : _this.controlHistorico[_idUnidadNegocio]);
    var controlAsignado = _this.controlAsignados[_turno.IdTurno];
    var controlHistorico = historico[_turno.IdTurno];

    if (!_isUndefined(controlAsignado)) {
        controlAsignado.historico = true;
    }

    if (_isUndefined(controlHistorico)) {
        controlHistorico = {
            'select': _this.refHistorico[_idUnidadNegocio],
            'turno': _turno,
            'reference': true,
            'historico': true
        };
    } else {
        controlHistorico.reference = true;
    }

    historico[_turno.IdTurno] = controlHistorico;
    _this.controlHistorico[_idUnidadNegocio] = historico;
};

Logic_TurnosManager.currentAsignado = function () {
    var _this = Logic_TurnosManager;
    var principal = null;
    var current = null;

    if (_this.changeReference(_this.controlAsignados, _this.refAsignado, false)) {
        _this.refAsignado = !_this.refAsignado;
    }

    $.each(_this.controlAsignados, function (_idTurno, _control) {
        if (!_control.solicitado) {
            principal = (_isNull(principal) ? _control : principal);
        }

        if (_control.select === _this.refAsignado && !_control.historico) {
            if (_isNull(current)) {
                _control.select = !_this.refAsignado;

                current = _control;
            }
        }
    });

    current = (_isNull(principal) ? current : principal);
    _this.asignadoBuffer = current;
};

Logic_TurnosManager.currentHistorico = function (_idUnidadNegocio) {
    var _this = Logic_TurnosManager;
    var historico = _this.controlHistorico[_idUnidadNegocio];
    var refHistorico = _this.refHistorico[_idUnidadNegocio];
    var current = null;

    if (!_isUndefined(historico)) {
        if (_this.changeReference(historico, refHistorico, true)) {
            _this.refHistorico[_idUnidadNegocio] = !refHistorico;
            refHistorico = _this.refHistorico[_idUnidadNegocio];
        }

        $.each(historico, function (_idTurno, _control) {
            if (_control.select === refHistorico) {
                if (_isNull(current)) {
                    _control.select = !refHistorico;

                    current = _control;
                }
            }
        });
    }

    _this.historicoBuffer[_idUnidadNegocio] = current;
};

Logic_TurnosManager.updateAsignado = function () {
    var _this = Logic_TurnosManager;
    var defaultView = App_DefaultView;
    var current = _this.asignadoBuffer;
    var turno;

    if (!_isUndefined(current) && !_isNull(current)) {
        turno = current.turno;

        if (!current.solicitado) {
            current.solicitado = true;

            _this.printTurnoSolicitado(turno);

            _beep();
        }

        defaultView.addTurnoNegocio(turno.IdUnidadNegocio, turno.IdTurno);
        defaultView.addEmpleado(turno.Empleado);
    } else {
        defaultView.addTurnoNegocio(-1);
        defaultView.addEmpleado();
    }
};

Logic_TurnosManager.updateHistorico = function (_idUnidadNegocio) {
    var _this = Logic_TurnosManager;
    var defaultView = App_DefaultView;
    var current = _this.historicoBuffer[_idUnidadNegocio];

    if (!_isUndefined(current) && !_isNull(current)) {
        defaultView.addTurnoUnidad(_idUnidadNegocio, current.turno.IdTurno);
    } else {
        defaultView.addTurnoUnidad(_idUnidadNegocio, -1);
    }
};

Logic_TurnosManager.changeReference = function (_controlBuffer, _reference, _historico) {
    var countSelected = 0;
    var countControl = 0;

    $.each(_controlBuffer, function (_idTurno, _control) {
        if (_control.historico === _historico) {
            countControl++;

            if (_control.select !== _reference) {
                countSelected++;
            }
        }
    });

    return (countSelected === countControl);
};

Logic_TurnosManager.checkBuffer = function (_elements) {
    if (!_isUndefined(_elements)) {
        $.each(_elements, function (_i, _element) {
            _element.reference = false;
        });
    }
};

Logic_TurnosManager.cleanBuffer = function (_elements) {
    var _this = Logic_TurnosManager;

    if (!_isUndefined(_elements)) {
        $.each(_elements, function (_i, _element) {
            if (_element.reference !== true) {
                delete _elements[_i];
            }
        });
    }
};

Logic_TurnosManager.printTurnoSolicitado = function (_turno) {
    var negocioClient = Client_NegocioClient;
    var message = _getMessage('turnoSolicitado');
    var unidadNegocio = negocioClient.unidadesNegocio[_turno.IdUnidadNegocio];

    message = message.replace('{descripcion}', unidadNegocio.Descripcion);
    message = message.replace('{idTurno}', _turno.IdTurno);

    _console(message);
};

// -------------------------------- logic/cjcr-turnos-manager.js [END]


// -------------------------------- app/cjcr-default-view.js [BEGIN]

var App_DefaultView = _package('App.DefaultView');

App_DefaultView.addPanelUnidades = function (_callback) {
    var negocioClient = Client_NegocioClient;
    var builder = Builder_DefaultBuilder;
    var panel = $('#panelUnidades');
    var part;

    negocioClient.getUnidadesActivas(function () {
        $.each(negocioClient.unidadesNegocio, function (_i, _unidadNegocio) {
            part = builder.getUnidadPanel(_unidadNegocio.IdUnidadNegocio);

            panel.append(part);

            if (!_isUndefined(_callback)) {
                _callback();
            }
        });
    }, null);
};

App_DefaultView.addTurnoNegocio = function (_idUnidadNegocio, _idTurno) {
    var builder = Builder_DefaultBuilder;
    var mensaje = builder.getMensajeAtencion(_idUnidadNegocio);
    var image = _get('imageUnidadDefault');
    var color = _get('colorUnidadDefault');
    var idTurno = _get('negocioNumberDefault');

    if (!_isUndefined(_idUnidadNegocio)) {
        unidadNegocio = Client_NegocioClient.unidadesNegocio[_idUnidadNegocio];

        if (!_isUndefined(unidadNegocio)) {
            image = builder.getImageUnidad(_idUnidadNegocio);
            color = unidadNegocio.Color;

            idTurno = (!_isUndefined(_idTurno) ? _leftPad(_idTurno, _get('padNumberDefault')) : idTurno);
        }
    }

    $('#mensajeAtencion').replaceWith(mensaje);
    $('#negocioImage').attr('src', image);
    $('#turnoNumber').text(idTurno);

    $('.datosDetail').css('color', color);
    $('.turnoNumber').css('color', color);
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

// -------------------------------- app/cjcr-default-view.js [END]


// -------------------------------- app/cjcr-application.js [BEGIN]

var App_Application = _package('App.Application');

$.support.cors = true;

$(function () {
    _init(App_Application.init);
});

App_Application.init = function () {
    var defaultView = App_DefaultView;
    var turnosManager = Logic_TurnosManager;

    defaultView.addTurnoNegocio(-1);
    defaultView.addPanelUnidades(turnosManager.initControl);

    turnosManager.init();
};

// -------------------------------- app/cjcr-application.js [END]
