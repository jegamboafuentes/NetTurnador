using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Baz.Caja.Commons.Collection;
using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Util;
using Baz.Caja.Commons.Web;
using Baz.Caja.Turnador.Logic;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Support;

namespace Baz.Caja.Turnador.Service
{
    public class CjCRTurnadorService : CjCRServiceBase
    {
        public CjCRTurnadorLogic TurnadorLogic { get; set; }

        public CjCRSummary ActivarContingencia(CjCRCredential credential)
        {
            return TurnadorLogic.ActivarContingencia(credential);
        }

        public CjCRSummary DesactivarContingencia(CjCRCredential credential)
        {
            return TurnadorLogic.DesactivarContingencia(credential);
        }

        public CjCRCajaConfig GetContingencia(CjCRCredential credential)
        {
            return TurnadorLogic.GetContingencia(credential);
        }

        public CjCRCajaConfig GetDisplayDuracion(CjCRCredential credential)
        {
            return TurnadorLogic.GetCajaConfig(CjCRTurnadorStatus.FOLIO_DISPLAY_DURACION, credential);
        }

        public CjCRCajaConfig GetTurnosCaducidad(CjCRCredential credential)
        {
            return TurnadorLogic.GetCajaConfig(CjCRTurnadorStatus.FOLIO_NO_TURNOS_CADUCIDAD, credential);
        }
    }
}
