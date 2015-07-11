using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Support;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Model;

namespace Baz.Caja.Turnador.Support
{
    public class CjCRTurnadorSummaryFactory : CjCRSummaryFactory
    {
        public override String GetDetail(int status)
        {
            String property;

            switch (status)
            {
                case CjCRTurnadorStatus.OPERATION_COMPLETE:
                    property = "operation.complete";
                    break;
                case CjCRTurnadorStatus.OPERATION_FAIL:
                    property = "operation.fail";
                    break;
                case CjCRTurnadorStatus.INCOMPLETE_OPERATION:
                    property = "incorrect.operation";
                    break;
                case CjCRTurnadorStatus.EMPLEADO_NO_ASIGNADO:
                    property = "empleado.no.asignado";
                    break;
                case CjCRTurnadorStatus.EMPLEADO_DIFERENTE_NEGOCIO:
                    property = "empleado.diferente.negocio";
                    break;
                case CjCRTurnadorStatus.TURNO_ASIGNADO_OTRO_EMPLEADO:
                    property = "turno.asignado.otro.empleado";
                    break;
                case CjCRTurnadorStatus.TURNO_ESTADO_INCORRECTO:
                    property = "turno.estado.incorrecto";
                    break;
                case CjCRTurnadorStatus.SIN_PLAN_CONTINGENCIA:
                    property = "sin.plan.contingencia";
                    break;
                case CjCRTurnadorStatus.EMPLEADO_NO_DISPONIBLE:
                    property = "empleado.no.disponible";
                    break;
                case CjCRTurnadorStatus.EMPLEADO_ESTADO_INCORRECTO:
                    property = "empleado.estado.incorrecto";
                    break;
                case CjCRTurnadorStatus.UNIDAD_NEGOCIO_INEXISTENTE:
                    property = "unidad.negocio.inexistente";
                    break;
                case CjCRTurnadorStatus.EMPLEADO_INEXISTENTE:
                    property = "empleado.inexistente";
                    break;
                case CjCRTurnadorStatus.UNIDAD_NEGOCIO_ACTIVA:
                    property = "unidad.negocio.activa";
                    break;
                case CjCRTurnadorStatus.UNIDAD_NEGOCIO_INACTIVA:
                    property = "unidad.negocio.inactiva";
                    break;

                default:
                    throw new ArgumentException(
                        CjCRPropertyUtils.Get("incorrect.status"));
            }

            return CjCRPropertyUtils.Get(property);
        }

        public CjCRSummaryTurno GetSummaryTurno(int status)
        {
            CjCRSummary summary = Get(status);
            CjCRSummaryTurno summaryTurno = new CjCRSummaryTurno();

            summaryTurno.Status = summary.Status;
            summaryTurno.Complete = summary.Complete;
            summaryTurno.Detail = summary.Detail;

            return summaryTurno;
        }
    }
}