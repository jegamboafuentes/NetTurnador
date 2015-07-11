<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CjCRTurnadorPanel._Default" %>
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta name="charset" content="utf-8"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <link href="resource/css/lib/bootstrap-update.css" rel="stylesheet"/>
    <link href="resource/css/lib/bootstrap-responsive.css" rel="stylesheet"/>
    <link href="resource/css/app/cjcr-turnador-panel.css" rel="stylesheet"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>   
    <title>Turnador | Display</title>
      <style type="text/css">
          .style1
          {
              font-weight: normal;
              font-size: 65px;
          }
          .style2
          {
              font-weight: normal;
              font-size: 46pt;
          }
      </style>
  </head>
  <body>
    <div class="superior izquierdo">
      <img class="bandaRoja" src="resource/img/rojo-pantalla.png"/>
      <p class="atendiendo">Espere su turno para ser atendido</p>
    </div>     
    <div class="superior derecho">
      <div class="centrarNegocio">
        <div id = "imagen" class="span7">             
             <img src="resource/img/hdtv-negframe.png" id="negocioImage" alt="Imagen de negocio"  
             class="img-rounded negocioImage" />
        </div>
      </div>
    </div>

    <div id="izquierda" class = "panel izquierdo">
        <div class="centrarNegocio">
        <div class="panelDetail">
            <div class="span8 detailCenter">
                <div class="container panelTurno">    
                    <div class="span5">
                            <p class="turnoTitle">TURNO</p>
                            <p id="turnoNumber" class="turnoNumber"></p>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </div>      
    <div id="derecha" class = "panel derecho">
        <div id="mensaje">
                        <div class="span6">
                            <p id="mensajeAtencion" class="mensajeDetail"></p>
                        </div>
        </div>
     </div>
 

    <div id="beepPanel"></div>
    
    <script type="text/javascript">
        var BASERESTSERVICE = '<%= this.Context.Items["NOMBRE"] %>';
<<<<<<< .mine
        var SERVER_NAME = '10.54.28.114';
        //var SERVER_NAME = '<%= this.Context.Items["SERVER_NAME"] %>';
=======
        var SERVER_NAME = '<%= this.Context.Items["SERVER_NAME"] %>';
>>>>>>> .r4269
    </script>
	<script src="resource/js/lib/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="resource/js/lib/bootstrap.js" type="text/javascript"></script>
    <script src="resource/js/lib/json2.js" type="text/javascript"></script>
    <script src="resource/js/lib/date.format.js" type="text/javascript"></script>
    <script src="resource/js/data/cjcr-application-data.js" type="text/javascript"></script>
    <script src="resource/js/data/cjcr-messages-data.js" type="text/javascript"></script>
    <script src="resource/js/support/cjcr-turnador-panel-status.js" type="text/javascript"></script>
    <script src="resource/js/support/cjcr-asserts.js" type="text/javascript"></script>
    <script src="resource/js/support/cjcr-app-core.js" type="text/javascript"></script>    
    <script src="resource/js/support/cjcr-app-config.js" type="text/javascript"></script>      
    <script src="resource/js/builder/cjcr-default-builder.js" type="text/javascript"></script>
    <script src="resource/js/client/cjcr-negocio-client.js" type="text/javascript"></script>
    <script src="resource/js/client/cjcr-turnos-client.js" type="text/javascript"></script>    
    <script src="resource/js/client/cjcr-turnador-client.js" type="text/javascript"></script>    
    <script src="resource/js/logic/cjcr-turnos-manager.js" type="text/javascript"></script>  
    <script src="resource/js/app/cjcr-default-view.js" type="text/javascript"></script>
    <script src="resource/js/app/cjcr-application.js" type="text/javascript"></script>
    
  </body>
</html>
