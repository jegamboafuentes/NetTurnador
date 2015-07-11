﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

using Baz.Caja.Commons.Collection;
using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Service;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Service;
using Baz.Caja.Turnador.Service.Ioc;

namespace Baz.Caja.Servicios
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]

    public class CjCRTurnadorService : CjCRServiceIoC<CjCRTurnadorServiceIoC>, CjCRITurnadorService
    {
        public CjCRSummary ActivarContingencia()
        {
            return Service.ActivarContingencia();
        }

        public CjCRSummary DesactivarContingencia()
        {
            return Service.DesactivarContingencia();
        }

        public CjCRCajaConfig GetContingencia()
        {
            return Service.GetContingencia();
        }

        public CjCRCajaConfig GetDisplayDuracion()
        {
            return Service.GetDisplayDuracion();
        }

        public CjCRCajaConfig GetTurnosCaducidad()
        {
            return Service.GetTurnosCaducidad();
        }
    }
}
