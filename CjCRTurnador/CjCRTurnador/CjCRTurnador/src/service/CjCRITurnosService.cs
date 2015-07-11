using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

using Baz.Caja.Commons.Model;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Commons.Collection;

namespace Baz.Caja.Turnador.Service
{
    [ServiceContract]
    public interface CjCRITurnosService
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "turno/", 
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json)]
        CjCRTurno GenerarTurno(CjCRTurno turno);

        [OperationContract]
        [WebGet(
            UriTemplate = "turno/{fecha}/{idUnidadNegocio}/{idTurno}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CjCRTurno GetTurno(String fecha,String idUnidadNegocio, String idTurno);

        [OperationContract]
        [WebGet(
            UriTemplate = "turno/{fecha}/{idUnidadNegocio}/{noEmpleado}/asignado/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CjCRTurno GetTurnoAsignado(String fecha, String idUnidadNegocio, String noEmpleado);

        [OperationContract]
        [WebGet(
            UriTemplate = "turno/{fecha}/negocio/asignados/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CjCRDictionary<Int32, List<CjCRTurno>> GetTurnosAsignados(String fecha);

        /// Servicio de turnos en atención.
        [OperationContract]
        [WebGet(
            UriTemplate = "turno/{fecha}/negocio/atendiendo/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CjCRDictionary<Int32, List<CjCRTurno>> GetTurnosAtendiendo(String fecha);

        [OperationContract]
        [WebGet(
            UriTemplate = "turno/{fecha}/negocio/historial/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CjCRDictionary<Int32, List<CjCRTurno>> GetTurnosHistorial(String fecha);


        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "turno/completar/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CjCRSummary CompletarTurnos();

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "turno/posponer/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CjCRSummary PosponerTurno(CjCRTurno turno);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "turno/cancelar/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CjCRSummary CancelarTurno(CjCRTurno turno);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "turno/{noEmpleado}/atender/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CjCRSummary AtenderTurno(String noEmpleado, CjCRTurno turno);

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "turno/virtual/{idUnidadNegocio}/{noEmpleado}/atender/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CjCRSummaryTurno AtenderTurnoVirtual(String idUnidadNegocio, String noEmpleado);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "turno/{noEmpleado}/apropiar/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CjCRSummary ApropiarTurno(String noEmpleado, CjCRTurno turno);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "turno/{noEmpleado}/finalizar/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CjCRSummary FinalizarTurno(String noEmpleado, CjCRTurno turno);


        [OperationContract]
        [WebGet(
            UriTemplate = "turno/{fecha}/{noEmpleado}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        CjCRTurno GetTurnoEmpleado(String fecha, String noEmpleado);
    }
}
