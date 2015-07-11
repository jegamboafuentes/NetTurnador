
var App_Application = _package('App.Application');

$.support.cors = true;

$(function () {
    _init(App_Application.init);
});

App_Application.init = function () {
    var defaultView = App_DefaultView;
    var turnosManager = Logic_TurnosManager;

    defaultView.addTurnoNegocio(-1);
    defaultView.addPanelUnidades();

    turnosManager.init();
};
