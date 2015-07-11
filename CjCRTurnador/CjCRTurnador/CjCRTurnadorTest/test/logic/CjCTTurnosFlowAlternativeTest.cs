using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Baz.Caja.Commons.Dao;
using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Service;
using Baz.Caja.Turnador.Support;
using Baz.Caja.Turnador.Test.Model;
using Baz.Caja.Turnador.Test.Logic;
using Baz.Caja.Turnador.Test.Support;

namespace Baz.Caja.Turnador.Test.Dao
{
    [TestClass()]
    public class CjCTTurnosFlowAlternativeTest
    {
        private static CjCRCredential DefaultCredential;
        private static CjCRTurnosService TurnosService;
        private static CjCRTurnadorService TurnadorService;        
        private static CjCRCajaConfigDao CajaConfigDao;
        private static CjCTTurnadorLogic TTurnadorLogic;
        private static CjCTTurnoXmlMapper TTurnoXmlMapper;
        private static CjCTTurnoApropiadoXmlMapper TTurnoApropiadoXmlMapper;

        public TestContext TestContext { get; set; }

        [ClassInitialize()]
        public static void ClassInit(TestContext testContext)
        {
            CjCRSpringContext.Init(
                "assembly://Baz.Caja.Turnador/Baz.Caja.Turnador.resource.config/application-context.xml",
                "assembly://Baz.Caja.Turnador.Test/Baz.Caja.Turnador.Test.resource.config/application-context-test.xml");

            DefaultCredential = CjCRSpringContext.Get<CjCRCredential>("DefaultCredential");
            TurnosService = CjCRSpringContext.Get<CjCRTurnosService>();
            TurnadorService = CjCRSpringContext.Get<CjCRTurnadorService>();            
            CajaConfigDao = CjCRSpringContext.Get<CjCRCajaConfigDao>();
            TTurnadorLogic = CjCRSpringContext.Get<CjCTTurnadorLogic>();
            TTurnoXmlMapper = CjCRSpringContext.Get<CjCTTurnoXmlMapper>();
            TTurnoApropiadoXmlMapper = CjCRSpringContext.Get<CjCTTurnoApropiadoXmlMapper>();
        }

        [TestMethod()]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\resource\\datasource\\TurnoTest.xml",
            "entrada",
            DataAccessMethod.Sequential)]
        public void AtenderVirtualTest()
        {
            CjCRTurno turno = TTurnoXmlMapper.Get(TestContext.DataRow,
                CjCTTurnoXmlMapper.BASIC_MODEL);
            CjCRSummaryTurno summaryTurno;
            CjCRSummary summary;
            bool expected= true;
            bool bResult = true;

            summary = TurnadorService.ActivarContingencia(DefaultCredential);
            bResult = (bResult && summary.Complete);

            bResult = (bResult && 
                TTurnadorLogic.SetDisponible(turno.IdUnidadNegocio, turno.Empleado.NoEmpleado));

            summaryTurno = TurnosService.AtenderTurnoVirtual(turno.IdUnidadNegocio.ToString(),
                turno.Empleado.NoEmpleado, DefaultCredential);
            bResult = (bResult && summaryTurno.Complete);

            bResult = (bResult && TTurnadorLogic.FinalizarTurno(summaryTurno.Turno));

            summary = TurnadorService.DesactivarContingencia(DefaultCredential);
            bResult = (bResult && summary.Complete);

            Assert.AreEqual<Boolean>(expected, bResult,
                "Turno virtual no atendido");
        }

        [TestMethod()]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\resource\\datasource\\TurnoTest.xml",
            "entrada",
            DataAccessMethod.Sequential)]
        public void CaducarTurnoTest()
        {
            CjCRCajaConfig cajaConfig = new CjCRCajaConfig(
                CjCRTurnadorStatus.MODULO_PROYECTO,
                CjCRTurnadorStatus.FOLIO_NO_TURNOS_CADUCIDAD);
            CjCRTurno turno = TTurnoXmlMapper.Get(TestContext.DataRow,
                CjCTTurnoXmlMapper.BASIC_MODEL);
            CjCRTTurnoStatusList turnosStatus;
            CjCRTTurnoStatus turnoStatus;
            CjCRSummary summary;
            bool expected = true;
            bool bResult = true;
            int no;

            cajaConfig = CajaConfigDao.GetConfiguracion(cajaConfig, DefaultCredential);
            no = CjCRParseUtils.ToInt32(cajaConfig.Valor);

            turnoStatus = TTurnadorLogic.PrepareTurno(turno);
            bResult = (bResult && turnoStatus.Status);

            turnosStatus = TTurnadorLogic.CreateTurnos(turno, no - 1);
            bResult = (bResult && turnosStatus.Status);

            turnoStatus = TTurnadorLogic.GetTurno(turnoStatus.Turno);
            bResult = (bResult && turnoStatus.Status);

            summary = TurnosService.PosponerTurno(turnoStatus.Turno, DefaultCredential);
            bResult = (bResult && summary.Complete);

            turnosStatus.AddElement(TTurnadorLogic.CreateTurno(turno));

            TTurnadorLogic.Delay();

            turnoStatus = TTurnadorLogic.GetTurno(turnoStatus.Turno);
            bResult = (bResult &&
                turnoStatus.Turno.Estado == CjCRTurnadorStatus.TURNO_CADUCADO);

            turnosStatus = TTurnadorLogic.SetTurnosEstado(turnosStatus.GetTurnos(),
                CjCRTurnadorStatus.TURNO_CANCELADO);
            bResult = (bResult && turnosStatus.Status);

            Assert.AreEqual<Boolean>(expected, bResult,
                "Turno no establecido como caducado");
        }

        [TestMethod()]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\resource\\datasource\\TurnoTest.xml",
            "entrada",
            DataAccessMethod.Sequential)]
        public void CancelarTurnoTest()
        {
            CjCRTurno turno = TTurnoXmlMapper.Get(TestContext.DataRow,
                CjCTTurnoXmlMapper.BASIC_MODEL);
            CjCRTTurnoStatus turnoStatus = TTurnadorLogic.PrepareTurno(turno);
            CjCRSummary summary;
            bool expected = true;
            bool bResult = true;

            summary = TurnosService.CancelarTurno(turnoStatus.Turno, DefaultCredential);
            bResult = (bResult && summary.Complete);

            Assert.AreEqual<Boolean>(expected, bResult,
                "Turnos no establecidos como cancelados");
        }

        [TestMethod()]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\resource\\datasource\\TurnoApropiadoTest.xml",
            "entrada",
            DataAccessMethod.Sequential)]
        public void ApropiarTurnoTest()
        {
            CjCTTurnoApropiado turnoApropiado = TTurnoApropiadoXmlMapper.Get(TestContext.DataRow,
                CjCTTurnoApropiadoXmlMapper.BASIC_MODEL);
            CjCRTTurnoStatus turnoStatus = TTurnadorLogic.PrepareTurno(turnoApropiado.Turno);
            CjCRSummary summary;
            bool expected = true;
            bool bResult = true;

            turnoApropiado.Turno = turnoStatus.Turno;
            bResult = (bResult && turnoStatus.Status);

            bResult = (bResult && TTurnadorLogic.SetDisponible(
                turnoApropiado.Turno.IdUnidadNegocio, turnoApropiado.NoEmpleadoAlterno));

            summary = TurnosService.AtenderTurno(turnoApropiado.NoEmpleadoAlterno,
                turnoApropiado.Turno, DefaultCredential);
            bResult = (bResult && summary.Status == CjCRTurnadorStatus.TURNO_ASIGNADO_OTRO_EMPLEADO);

            turnoStatus = TTurnadorLogic.GetTurno(turnoApropiado.Turno);
            turnoApropiado.Turno = turnoStatus.Turno;
            bResult = (bResult && turnoStatus.Status);

            summary = TurnosService.ApropiarTurno(turnoApropiado.NoEmpleadoAlterno,
                turnoApropiado.Turno, DefaultCredential);
            bResult = (bResult && summary.Complete);
            
            bResult = (bResult && TTurnadorLogic.FinalizarTurno(turnoApropiado.Turno));

            Assert.AreEqual<Boolean>(expected, bResult,
                "Turno no apropiado no atendido");
        }
    }
}
