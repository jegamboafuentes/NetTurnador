using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

using Baz.Caja.Commons.Util;
using Baz.Caja.Commons.Support;

namespace Baz.Caja.Turnador.Support
{
    public class CjCRAppConfig
    {
        public CjCRStateMachinePool EstadosTurnoPool { get; set; }
        public CjCRStateMachinePool EstadosAtencionPool { get; set; }
        public bool Production { get; set; }

        private bool bInit;

        public void Init()
        {
            if (!bInit)
            {
                bInit = true;

                InitMessages();
                InitStateMachines();
            }
        }

        private void InitMessages()
        {
            CjCRPropertyUtils.FileDefault = "messages";
            
            CjCRPropertyUtils.InitAssembly("messages", Assembly.GetExecutingAssembly(), 
                "Baz.Caja.Turnador.resource.properties.messages.properties");
        }

        private void InitStateMachines()
        {
            EstadosTurnoPool.AddState(
                CjCRTurnadorStatus.TURNO_GENERADO);

            EstadosTurnoPool.AddNext(
                CjCRTurnadorStatus.TURNO_GENERADO,
                CjCRTurnadorStatus.TURNO_ASIGNADO);
            EstadosTurnoPool.AddNext(
                CjCRTurnadorStatus.TURNO_GENERADO,
                CjCRTurnadorStatus.TURNO_CANCELADO);

            EstadosTurnoPool.AddNext(
                CjCRTurnadorStatus.TURNO_ASIGNADO,
                CjCRTurnadorStatus.TURNO_EN_ATENCION);
            EstadosTurnoPool.AddNext(
                CjCRTurnadorStatus.TURNO_ASIGNADO,
                CjCRTurnadorStatus.TURNO_POSPUESTO);
            EstadosTurnoPool.AddNext(
                CjCRTurnadorStatus.TURNO_ASIGNADO,
                CjCRTurnadorStatus.TURNO_CANCELADO);

            EstadosTurnoPool.AddNext(
                CjCRTurnadorStatus.TURNO_EN_ATENCION,
                CjCRTurnadorStatus.TURNO_ATENDIDO);
            EstadosTurnoPool.AddNext(
                CjCRTurnadorStatus.TURNO_EN_ATENCION,
                CjCRTurnadorStatus.TURNO_POSPUESTO);

            EstadosTurnoPool.AddNext(
                CjCRTurnadorStatus.TURNO_POSPUESTO,
                CjCRTurnadorStatus.TURNO_CADUCADO);
            EstadosTurnoPool.AddNext(
                CjCRTurnadorStatus.TURNO_POSPUESTO,
                CjCRTurnadorStatus.TURNO_EN_ATENCION);

            EstadosAtencionPool.AddState(
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE);

            EstadosAtencionPool.AddNext(
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE);
            EstadosAtencionPool.AddNext(
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE, 
                CjCRTurnadorStatus.ATENCION_DISPONIBLE);

            EstadosAtencionPool.AddNext(
                CjCRTurnadorStatus.ATENCION_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_DISPONIBLE);
            EstadosAtencionPool.AddNext(
                CjCRTurnadorStatus.ATENCION_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_ASIGNADO);
            EstadosAtencionPool.AddNext(
                CjCRTurnadorStatus.ATENCION_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_OCUPADO);
            EstadosAtencionPool.AddNext(
                CjCRTurnadorStatus.ATENCION_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE);

            EstadosAtencionPool.AddNext(
                CjCRTurnadorStatus.ATENCION_ASIGNADO,
                CjCRTurnadorStatus.ATENCION_OCUPADO);
            EstadosAtencionPool.AddNext(
                CjCRTurnadorStatus.ATENCION_ASIGNADO,
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE);

            EstadosAtencionPool.AddNext(
                CjCRTurnadorStatus.ATENCION_OCUPADO,
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE);
        }
    }
}
