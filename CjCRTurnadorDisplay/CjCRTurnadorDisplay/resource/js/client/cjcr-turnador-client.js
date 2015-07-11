
var Client_TurnadorClient = _package('Client.TurnadorClient');

Client_TurnadorClient.GET_DISPLAY_DURACION = 0;

Client_TurnadorClient.getDisplayDuracion = function (_callback, _callbackError) {
    var _this = Client_TurnadorClient;

    _this.callService(_this.GET_DISPLAY_DURACION, _callback, _callbackError);
};

Client_TurnadorClient.getDisplayDuracionSuccess = function (_data, _status, _jqXHR, _callback) {
    var _this = Client_TurnadorClient;

    _assertUndefined(_callback, 'Callback function is undefined');

    _callback(_data);
};


Client_TurnadorClient.callService = function (_service, _callback, _callbackError, _data, _paths) {
    var _this = Client_TurnadorClient;

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

Client_TurnadorClient.callServiceSuccess = function (_data, _status, _jqXHR, _service, _callback) {
    var _this = Client_TurnadorClient;

    switch (_service) {
        case _this.GET_DISPLAY_DURACION:
            return _this.getDisplayDuracionSuccess(_data, _status, _jqXHR, _callback);
        default:
            _fail('Invalid service');
    }
};

Client_TurnadorClient.callServiceError = function (_jqXHR, _status, _error,
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

Client_TurnadorClient.getType = function (_service) {
    var _this = Client_TurnadorClient;

    switch (_service) {
        case _this.GET_DISPLAY_DURACION:
        return 'GET';
        default:
            _fail('Invalid service');
    }
};

Client_TurnadorClient.getUrl = function (_service, _paths) {
    var _this = Client_TurnadorClient;
    var url = _get('turnadorServiceBase');

    switch (_service) {
        case _this.GET_DISPLAY_DURACION:
            url = url.concat('turnador/display/duracion/');
            break;
        default:
            _fail('Invalid service');
    }

    return url;
};
