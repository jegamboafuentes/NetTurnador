using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Web;

using Baz.Caja.Commons.Collection;
using Baz.Caja.Commons.Model;
using Baz.Caja.Servicios;
using Baz.Caja.Turnador.Model;

namespace Baz.Caja.Turnador.Service.Ioc
{
    public class CjCRTurnadorServiceIoC : CjCRITurnadorService
    {   
        public CjCRTurnadorService TurnadorService { get; set; }

        public CjCRSummary ActivarContingencia()
        {
            return TurnadorService.ActivarContingencia(
                TurnadorService.GetAuthorization(true));
        }

        public CjCRSummary DesactivarContingencia()
        {
            return TurnadorService.DesactivarContingencia(
                TurnadorService.GetAuthorization(true));
        }

        public CjCRCajaConfig GetContingencia()
        {
            return TurnadorService.GetContingencia(
                TurnadorService.GetDefaultAuthorization(true));
        }

        public CjCRCajaConfig GetDisplayDuracion()
        {
            return TurnadorService.GetDisplayDuracion(
                TurnadorService.GetDefaultAuthorization(true));
        }

        public CjCRCajaConfig GetTurnosCaducidad()
        {
            return TurnadorService.GetTurnosCaducidad(
                TurnadorService.GetDefaultAuthorization(true));
        }
    }
}
