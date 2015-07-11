
var Client_TurnosClient = _package('Client.TurnosClient');

Client_TurnosClient.turnosNegocioAsignados = {};
Client_TurnosClient.turnosNegocioHistorial = {};

Client_TurnosClient.TURNOS_HISTORIAL = 4;

Client_TurnosClient.TURNOS_ASIGNADOS = 3;

Client_TurnosClient.getTurnosAsignados = function (_callback, _callbackError) {
    var _this = Client_TurnosClient;
    var fecha = new Date();

    _this.callService(_this.TURNOS_ASIGNADOS, _callback, _callbackError, null, {
        fecha: fecha.format('yyyymmdd')
    });
};

/// Turnos en atencion.
Client_TurnosClient.getTurnosHistorial = function (_callback, _callbackError) {
    var _this = Client_TurnosClient;
    var fecha = new Date();

    _this.callService(_this.TURNOS_HISTORIAL, _callback, _callbackError, null, {
        fecha: fecha.format('yyyymmdd')
    });
};

Client_TurnosClient.getTurnosAsignadosSuccess = function (_data, _status, _jqXHR, _callback) {
    var _this = Client_TurnosClient;

    _this.turnosNegocioAsignados = {};

    _assertUndefined(_callback, 'Callback function is undefined');

    $.each(_data, function (_i, _entry) {
        _this.turnosNegocioAsignados[_entry.Key] = _entry.Value;
    });

    _callback(_data);
};

/// Turnos en atencion.
Client_TurnosClient.getTurnosHistorialSuccess = function (_data, _status, _jqXHR, _callback) {
    var _this = Client_TurnosClient;

    _this.turnosNegocioHistorial = {};

    _assertUndefined(_callback, 'Callback function is undefined');

    $.each(_data, function (_i, _entry) {
        _this.turnosNegocioHistorial[_entry.Key] = _entry.Value;
    });

    _callback(_data);
};


////no cambiar
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
        case _this.TURNOS_HISTORIAL:
            return _this.getTurnosHistorialSuccess(_data, _status, _jqXHR, _callback);
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

    _console(message);

    if (!_isUndefined(_callbackError) && !_isNull(_callbackError)) {
        _callbackError();
    }
};

Client_TurnosClient.getType = function (_service) {
    var _this = Client_TurnosClient;

    switch (_service) {
        case _this.TURNOS_ASIGNADOS:
            return 'GET';
        case _this.TURNOS_HISTORIAL:
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
        case _this.TURNOS_HISTORIAL:
            url = url.concat('turno/{fecha}/negocio/historial/');
            url = url.replace('{fecha}', _paths.fecha);
            break;
        default:
            _fail('Invalid service');
    }

    return url;
};

 