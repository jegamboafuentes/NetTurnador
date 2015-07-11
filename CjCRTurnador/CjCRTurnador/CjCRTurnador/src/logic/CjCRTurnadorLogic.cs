using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Baz.Caja.Commons.Collection;
using Baz.Caja.Commons.Dao;
using Baz.Caja.Commons.Model;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Dao;
using Baz.Caja.Turnador.Support;

namespace Baz.Caja.Turnador.Logic
{
    public class CjCRTurnadorLogic
    {
        public CjCRCajaConfigDao CajaConfigDao { get; set; }
        public CjCRTurnadorSummaryFactory TurnadorSummaryFactory { get; set; }

        public CjCRSummary ActivarContingencia(CjCRCredential credential)
        {
            return ActualizarPlanContingencia(
                CjCRTurnadorStatus.PLAN_CONTINGENCIA_ACTIVO, credential);
        }

        public CjCRSummary DesactivarContingencia(CjCRCredential credential)
        {
            return ActualizarPlanContingencia(
                CjCRTurnadorStatus.PLAN_CONTINGENCIA_INACTIVO, credential);
        }

        public CjCRCajaConfig GetContingencia(CjCRCredential credential)
        {
            CjCRCajaConfig cajaConfig = GetPlanContingenciaConfig();
            
            cajaConfig = CajaConfigDao.GetConfiguracion(cajaConfig, credential);

            return cajaConfig;
        }

        public CjCRCajaConfig GetCajaConfig(int folio, CjCRCredential credential)
        {
            CjCRCajaConfig cajaConfig = new CjCRCajaConfig(
                CjCRTurnadorStatus.MODULO_PROYECTO, folio);

            return CajaConfigDao.GetConfiguracion(cajaConfig, credential);
        }

        private CjCRSummary ActualizarPlanContingencia(int action, CjCRCredential credential)
        {
            CjCRCajaConfig cajaConfig = GetPlanContingenciaConfig();
            int status;

            cajaConfig.Valor = action.ToString();
            status = CajaConfigDao.UpdateConfiguracion(cajaConfig, credential);

            return TurnadorSummaryFactory.Get(status);
        }

        public CjCRCajaConfig GetPlanContingenciaConfig()
        {
            CjCRCajaConfig cajaConfig = new CjCRCajaConfig();

            cajaConfig.Modulo = CjCRTurnadorStatus.MODULO_PROYECTO;
            cajaConfig.Folio = CjCRTurnadorStatus.FOLIO_PLAN_CONTINGENCIA;

            return cajaConfig;
        }
    }
}
