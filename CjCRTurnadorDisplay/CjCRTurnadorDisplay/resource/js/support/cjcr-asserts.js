
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
