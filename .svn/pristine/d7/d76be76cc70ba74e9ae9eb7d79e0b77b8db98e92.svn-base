
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

function _console(_message) {
    if (!_get('production')) {
        console.log(_message);
    }
}
