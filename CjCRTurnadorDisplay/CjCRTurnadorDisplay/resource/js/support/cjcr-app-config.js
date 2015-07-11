
var _INIT_LOAD_CONFIG = 1;

function _init(_callback) {
    var appData = App_Data;
    var turnadorClient = Client_TurnadorClient;
    
    appData.loadFinished = 0;
    appData.appConfig = APPLICATION_DATA;
    appData.messages = MESSAGES_DATA;

    turnadorClient.getDisplayDuracion(function (_data) {
        appData.appConfig.turnoClientTaskTime = parseInt(_data.Valor) * _get('milliseconds');

        _initCallback(_callback);

    });
    
   

}

function _initCallback(_callback) {
    var appData = App_Data;

    appData.loadFinished++;

    if (appData.loadFinished === _INIT_LOAD_CONFIG) {
        _callback();
    }
}
