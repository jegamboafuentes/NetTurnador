using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Service;

namespace Baz.Caja.Turnador.Test.Dao
{
    [TestClass]
    public class CjCTNegocioFlowTest
    {
        private static CjCRCredential DefaultCredential;
        private static CjCRNegocioService NegocioService;

        public TestContext TestContext { get; set; }

        [ClassInitialize()]
        public static void ClassInit(TestContext testContext) 
        {
            CjCRSpringContext.Init(
                "assembly://Baz.Caja.Turnador/Baz.Caja.Turnador.resource.config/application-context.xml");

            DefaultCredential = CjCRSpringContext.Get<CjCRCredential>("DefaultCredential");
            NegocioService = CjCRSpringContext.Get<CjCRNegocioService>();
        }

        [TestMethod]
        public void GetUnidadesNegocioTest()
        {
            List<CjCRUnidadNegocio> unidadesNegocio = NegocioService.GetUnidadesNegocio(
                DefaultCredential);
            bool expected = true;
            bool bResult = true;

            bResult = (bResult && (unidadesNegocio.Count > 0));

            Assert.AreEqual<Boolean>(expected, bResult,
                "Lista de unidades de negocio vac\u00EDa");
        }
    }
}
