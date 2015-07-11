
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

    _console(message);

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
