using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Support.Test;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Dao;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Support;
using Baz.Caja.Turnador.Test.Logic;
using Baz.Caja.Turnador.Test.Support;

namespace Baz.Caja.Turnador.Test.Dao
{    
    [TestClass()]
    public class CjCTPoolFlowTest
    {
        private static CjCRCredential DefaultCredential;
        private static CjCTTurnadorLogic TTurnadorLogic;
        private static CjCRPoolDao PoolDao;
        private static CjCTTurnoXmlMapper TTurnoXmlMapper;
        private static CjCTEmpleadoPoolXmlMapper TEmpleadoPoolXmlMapper;

        public TestContext TestContext { get; set; }

        [ClassInitialize()]
        public static void ClassInit(TestContext testContext) 
        {
            CjCRSpringContext.Init(
                "assembly://Baz.Caja.Turnador/Baz.Caja.Turnador.resource.config/application-context.xml",
                "assembly://Baz.Caja.Turnador.Test/Baz.Caja.Turnador.Test.resource.config/application-context-test.xml");

            DefaultCredential = CjCRSpringContext.Get<CjCRCredential>("DefaultCredential");
            TTurnadorLogic = CjCRSpringContext.Get<CjCTTurnadorLogic>();
            PoolDao = CjCRSpringContext.Get<CjCRPoolDao>();
            TTurnoXmlMapper = CjCRSpringContext.Get<CjCTTurnoXmlMapper>();
            TEmpleadoPoolXmlMapper = CjCRSpringContext.Get<CjCTEmpleadoPoolXmlMapper>();
        }

        [TestMethod]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\resource\\datasource\\EmpleadoPoolTest.xml",
            "entrada",
            DataAccessMethod.Sequential)]
        public void SetEstadoAtencionTest()
        {
            string origen = "SetEstadoAtencionTest";
            CjCREmpleadoPool empleadoPool = TEmpleadoPoolXmlMapper.Get(
                TestContext.DataRow, CjCTEmpleadoPoolXmlMapper.BASIC_MODEL);
            CjCRGenericDataRowMapper mapper = new CjCRGenericDataRowMapper(
                TestContext.DataRow);
            bool expected = mapper.GetBoolean("expected");
            bool bResult = true;

            bResult = (bResult && 
                (CjCRTurnadorStatus.OPERATION_COMPLETE == PoolDao.SetEstado(
                    empleadoPool, origen, DefaultCredential)));

            Assert.AreEqual<Boolean>(expected, bResult, 
                "Asignaci\u00F3n incorrecta de estado");
        }

        [TestMethod]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\resource\\datasource\\TurnoTest.xml",
            "entrada",
            DataAccessMethod.Sequential)]
        public void GetPoolAtencionTest()
        {
            CjCRTurno turno = TTurnoXmlMapper.Get(TestContext.DataRow, 
                CjCTTurnoXmlMapper.BASIC_MODEL);
            List<CjCREmpleadoPool> pool;
            bool expected = true;
            bool bResult = true;

            bResult = (bResult && TTurnadorLogic.SetDisponible(
                turno.IdUnidadNegocio, turno.Empleado.NoEmpleado));
            pool = PoolDao.GetPool(DefaultCredential);

            bResult = (bResult && (pool.Count > 0));

            Assert.AreEqual<Boolean>(expected, bResult, 
                "Lista de empleados en pool vac\u00EDa");
        }

        [TestMethod]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\resource\\datasource\\TurnoTest.xml",
            "entrada",
            DataAccessMethod.Sequential)]
        public void GetPoolByEmpleadoTest()
        {
            CjCRTurno turno = TTurnoXmlMapper.Get(TestContext.DataRow,
                CjCTTurnoXmlMapper.BASIC_MODEL);
            CjCREmpleadoPool empleadoPool;
            bool expected = true;
            bool bResult = true;

            bResult = (bResult && TTurnadorLogic.SetDisponible(
                turno.IdUnidadNegocio, turno.Empleado.NoEmpleado));

            empleadoPool = PoolDao.GetPoolByEmpleado(turno.Empleado.NoEmpleado,
                DefaultCredential);
            bResult = (bResult && (empleadoPool != default(CjCREmpleadoPool)));

            Assert.AreEqual<Boolean>(expected, bResult, 
                "Empleado en pool no encontrado");
        }

        [TestMethod]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\resource\\datasource\\TurnoTest.xml",
            "entrada",
            DataAccessMethod.Sequential)]
        public void GetPoolByUnidadTest()
        {
            CjCRTurno turno = TTurnoXmlMapper.Get(TestContext.DataRow,
                CjCTTurnoXmlMapper.BASIC_MODEL);
            List<CjCREmpleadoPool> pool;
            bool expected = true;
            bool bResult = true;

            bResult = (bResult && TTurnadorLogic.SetDisponible(
                turno.IdUnidadNegocio, turno.Empleado.NoEmpleado));

            pool = PoolDao.GetPoolByUnidad(turno.IdUnidadNegocio, DefaultCredential);
            bResult = (bResult && (pool.Count > 0));

            Assert.AreEqual<Boolean>(expected, bResult,
                "Lista de empleados en pool por unidad vac\u00EDa");
        }
    }
}
