using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Baz.Caja.Commons.Collection;
using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Dao;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Service;
using Baz.Caja.Turnador.Support;
using Baz.Caja.Turnador.Test.Logic;
using Baz.Caja.Turnador.Test.Support;

namespace Baz.Caja.Turnador.Test.Dao
{    
    [TestClass()]
    public class CjCTTurnosFlowBasicTest
    {
        private static CjCRCredential DefaultCredential;
        private static CjCRTurnosService TurnosService;        
        private static CjCRTurnosDao TurnosDao;
        private static CjCTTurnadorLogic TTurnadorLogic;
        private static CjCTTurnoXmlMapper TTurnoXmlMapper;

        public TestContext TestContext { get; set; }
        
        [ClassInitialize()]
        public static void ClassInit(TestContext testContext)
        {
            CjCRSpringContext.Init(
                "assembly://Baz.Caja.Turnador/Baz.Caja.Turnador.resource.config/application-context.xml",
                "assembly://Baz.Caja.Turnador.Test/Baz.Caja.Turnador.Test.resource.config/application-context-test.xml");

            DefaultCredential = CjCRSpringContext.Get<CjCRCredential>("DefaultCredential");
            TurnosService = CjCRSpringContext.Get<CjCRTurnosService>();
            TTurnadorLogic = CjCRSpringContext.Get<CjCTTurnadorLogic>();
            TurnosDao = CjCRSpringContext.Get<CjCRTurnosDao>();
            TTurnoXmlMapper = CjCRSpringContext.Get<CjCTTurnoXmlMapper>();
        }

        [TestMethod()]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\resource\\datasource\\TurnoTest.xml",
            "entrada",
            DataAccessMethod.Sequential)]
        public void AtencionTurnoTest()
        {
            CjCRTurno turno = TTurnoXmlMapper.Get(TestContext.DataRow,
                CjCTTurnoXmlMapper.BASIC_MODEL);
            CjCRTTurnoStatus turnoStatus = TTurnadorLogic.PrepareTurno(turno);
            bool expected = true;
            bool bResult = true;

            bResult = (bResult && turnoStatus.Status);

            turnoStatus = TTurnadorLogic.GetTurno(turnoStatus.Turno);
            bResult = (bResult && turnoStatus.Status);

            turno = turnoStatus.Turno;
            bResult = (bResult && turno.Empleado != default(CjCREmpleadoPool));

            bResult = (bResult && TTurnadorLogic.AtenderFinalizarTurno(turno));

            Assert.AreEqual<Boolean>(expected, bResult,
                "No se realiz\u00F3 la atenci\u00F3n del turno");
        }

        [TestMethod()]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\resource\\datasource\\TurnoTest.xml",
            "entrada",
            DataAccessMethod.Sequential)]
        public void GenerarTurnoTest()
        {
            CjCRTurno turno = TTurnoXmlMapper.Get(TestContext.DataRow,
                CjCTTurnoXmlMapper.BASIC_MODEL);
            CjCRTTurnoStatus turnoStatus = TTurnadorLogic.GetTurnoEstado(turno,
                CjCRTurnadorStatus.TURNO_CANCELADO);            
            bool bResult = turnoStatus.Status;
            bool expected = true;

            Assert.AreEqual<Boolean>(expected, bResult,
                "Turno no generado");
        }

        [TestMethod()]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\resource\\datasource\\TurnoTest.xml",
            "entrada",
            DataAccessMethod.Sequential)]
        public void GetTurnosAsignadosTest()
        {
            CjCRTurno turno = TTurnoXmlMapper.Get(TestContext.DataRow,
                CjCTTurnoXmlMapper.BASIC_MODEL);
            CjCRDictionary<Int32, List<CjCRTurno>> turnos;
            CjCRTTurnoStatus turnoStatus = TTurnadorLogic.PrepareTurno(turno);
            bool expected = true;
            bool bResult = true;

            turno = turnoStatus.Turno;
            bResult = (bResult && turnoStatus.Status);

            turnos = TurnosService.GetTurnosAsignados(turno.Fecha.ToString(),
                DefaultCredential);
            bResult = (bResult && turnos.Count > 0);

            turnoStatus = TTurnadorLogic.SetTurnoEstado(turno, CjCRTurnadorStatus.TURNO_CANCELADO);
            bResult = (bResult && turnoStatus.Status);

            Assert.AreEqual<Boolean>(expected, bResult,
                "Listas de turnos vac\u00EDas");
        }

        [TestMethod()]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\resource\\datasource\\TurnoTest.xml",
            "entrada",
            DataAccessMethod.Sequential)]
        public void GetTurnosEstadoTest()
        {
            CjCRTurno turno = TTurnoXmlMapper.Get(TestContext.DataRow,
                CjCTTurnoXmlMapper.BASIC_MODEL);
            CjCRTTurnoStatus turnoStatus = TTurnadorLogic.GetTurnoEstado(turno, turno.Estado);
            bool bResult = turnoStatus.Status;
            bool expected = true;
            List<CjCRTurno> turnos;

            turno = turnoStatus.Turno;
            turnos = TurnosDao.GetTurnos(turno.Fecha, turno.IdUnidadNegocio,
                turno.Estado, DefaultCredential);
            bResult = (bResult && (turnos.Count > 0));

            Assert.AreEqual<Boolean>(expected, bResult, 
                "Lista de turnos vac\u00EDa");
        }
    }
}
