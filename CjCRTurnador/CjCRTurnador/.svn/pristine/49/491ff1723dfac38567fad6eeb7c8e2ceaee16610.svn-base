using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Baz.Caja.Commons.Support;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Support;

namespace Baz.Caja.Turnador.Test.Support
{
    [TestClass()]
    public class CjCTAppConfigTest
    {
        private static CjCRStateMachinePool EstadosTurnoPool;
        private static CjCRStateMachinePool EstadosAtencionPool;

        public TestContext TestContext { get; set; }

        [ClassInitialize()]
        public static void ClassInit(TestContext testContext)
        {
            CjCRSpringContext.Init(
                "assembly://Baz.Caja.Turnador/Baz.Caja.Turnador.resource.config/application-context.xml");

            EstadosTurnoPool = CjCRSpringContext.Get<CjCRStateMachinePool>("EstadosTurnoPool");
            EstadosAtencionPool = CjCRSpringContext.Get<CjCRStateMachinePool>("EstadosAtencionPool");
        }

        [TestMethod()]
        public void EstadosTurnoTest()
        {
            bool expected = true;
            bool bResult = true;

            bResult = (bResult && EstadosTurnoPool.IsNext(
                CjCRTurnadorStatus.TURNO_GENERADO,
                CjCRTurnadorStatus.TURNO_ASIGNADO));
            bResult = (bResult && EstadosTurnoPool.IsNext(
                CjCRTurnadorStatus.TURNO_GENERADO,
                CjCRTurnadorStatus.TURNO_CANCELADO));

            bResult = (bResult && EstadosTurnoPool.IsNext(
                CjCRTurnadorStatus.TURNO_ASIGNADO,
                CjCRTurnadorStatus.TURNO_EN_ATENCION));
            bResult = (bResult && EstadosTurnoPool.IsNext(
                CjCRTurnadorStatus.TURNO_ASIGNADO,
                CjCRTurnadorStatus.TURNO_POSPUESTO));
            bResult = (bResult && EstadosTurnoPool.IsNext(
                CjCRTurnadorStatus.TURNO_ASIGNADO,
                CjCRTurnadorStatus.TURNO_CANCELADO));

            bResult = (bResult && EstadosTurnoPool.IsNext(
                CjCRTurnadorStatus.TURNO_EN_ATENCION,
                CjCRTurnadorStatus.TURNO_ATENDIDO));
            bResult = (bResult && EstadosTurnoPool.IsNext(
                CjCRTurnadorStatus.TURNO_EN_ATENCION,
                CjCRTurnadorStatus.TURNO_POSPUESTO));

            bResult = (bResult && EstadosTurnoPool.IsNext(
                CjCRTurnadorStatus.TURNO_POSPUESTO,
                CjCRTurnadorStatus.TURNO_CADUCADO));
            bResult = (bResult && EstadosTurnoPool.IsNext(
                CjCRTurnadorStatus.TURNO_POSPUESTO,
                CjCRTurnadorStatus.TURNO_EN_ATENCION));

            bResult = (bResult && EstadosTurnoPool.IsPrevious(
                CjCRTurnadorStatus.TURNO_CADUCADO,
                CjCRTurnadorStatus.TURNO_POSPUESTO));

            bResult = (bResult && EstadosTurnoPool.IsPrevious(
                CjCRTurnadorStatus.TURNO_POSPUESTO,
                CjCRTurnadorStatus.TURNO_ASIGNADO));
            bResult = (bResult && EstadosTurnoPool.IsPrevious(
                CjCRTurnadorStatus.TURNO_POSPUESTO,
                CjCRTurnadorStatus.TURNO_EN_ATENCION));

            bResult = (bResult && EstadosTurnoPool.IsPrevious(
                CjCRTurnadorStatus.TURNO_EN_ATENCION,
                CjCRTurnadorStatus.TURNO_POSPUESTO));
            bResult = (bResult && EstadosTurnoPool.IsPrevious(
                CjCRTurnadorStatus.TURNO_EN_ATENCION,
                CjCRTurnadorStatus.TURNO_ASIGNADO));

            bResult = (bResult && EstadosTurnoPool.IsPrevious(
                CjCRTurnadorStatus.TURNO_ATENDIDO,
                CjCRTurnadorStatus.TURNO_EN_ATENCION));

            bResult = (bResult && EstadosTurnoPool.IsPrevious(
                CjCRTurnadorStatus.TURNO_ASIGNADO,
                CjCRTurnadorStatus.TURNO_GENERADO));

            bResult = (bResult && EstadosTurnoPool.IsPrevious(
                CjCRTurnadorStatus.TURNO_CANCELADO,
                CjCRTurnadorStatus.TURNO_ASIGNADO));
            bResult = (bResult && EstadosTurnoPool.IsPrevious(
                CjCRTurnadorStatus.TURNO_CANCELADO,
                CjCRTurnadorStatus.TURNO_GENERADO));

            Assert.AreEqual<Boolean>(expected, bResult,
                "La configuraci\u00F3n de estados de turno no est\u00E1 definida adecuadamente");
        }

        [TestMethod()]
        public void EstadosAtencionTest()
        {
            bool expected = true;
            bool bResult = true;

            bResult = (bResult && EstadosAtencionPool.IsNext(
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE));
            bResult = (bResult && EstadosAtencionPool.IsNext(
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_DISPONIBLE));

            bResult = (bResult && EstadosAtencionPool.IsNext(
                CjCRTurnadorStatus.ATENCION_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_DISPONIBLE));
            bResult = (bResult && EstadosAtencionPool.IsNext(
                CjCRTurnadorStatus.ATENCION_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_ASIGNADO));
            bResult = (bResult && EstadosAtencionPool.IsNext(
                CjCRTurnadorStatus.ATENCION_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_OCUPADO));
            bResult = (bResult && EstadosAtencionPool.IsNext(
                CjCRTurnadorStatus.ATENCION_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE));

            bResult = (bResult && EstadosAtencionPool.IsNext(
                CjCRTurnadorStatus.ATENCION_ASIGNADO,
                CjCRTurnadorStatus.ATENCION_OCUPADO));
            bResult = (bResult && EstadosAtencionPool.IsNext(
                CjCRTurnadorStatus.ATENCION_ASIGNADO,
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE));

            bResult = (bResult && EstadosAtencionPool.IsNext(
                CjCRTurnadorStatus.ATENCION_OCUPADO,
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE));

            bResult = (bResult && EstadosAtencionPool.IsPrevious(
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE));
            bResult = (bResult && EstadosAtencionPool.IsPrevious(
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_DISPONIBLE));
            bResult = (bResult && EstadosAtencionPool.IsPrevious(
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_ASIGNADO));
            bResult = (bResult && EstadosAtencionPool.IsPrevious(
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_OCUPADO));

            bResult = (bResult && EstadosAtencionPool.IsPrevious(
                CjCRTurnadorStatus.ATENCION_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_DISPONIBLE));
            bResult = (bResult && EstadosAtencionPool.IsPrevious(
                CjCRTurnadorStatus.ATENCION_DISPONIBLE,
                CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE));

            bResult = (bResult && EstadosAtencionPool.IsPrevious(
                CjCRTurnadorStatus.ATENCION_ASIGNADO,
                CjCRTurnadorStatus.ATENCION_DISPONIBLE));

            bResult = (bResult && EstadosAtencionPool.IsPrevious(
                CjCRTurnadorStatus.ATENCION_OCUPADO,
                CjCRTurnadorStatus.ATENCION_DISPONIBLE));
            bResult = (bResult && EstadosAtencionPool.IsPrevious(
                CjCRTurnadorStatus.ATENCION_OCUPADO,
                CjCRTurnadorStatus.ATENCION_ASIGNADO));

            Assert.AreEqual<Boolean>(expected, bResult,
                "La configuraci\u00F3n de estados de atenci\u00F3n no est\u00E1 definida adecuadamente");
        }
    }
}
