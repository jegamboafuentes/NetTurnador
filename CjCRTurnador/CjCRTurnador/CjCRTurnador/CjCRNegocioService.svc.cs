using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

using Baz.Caja.Commons.Service;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Service;
using Baz.Caja.Turnador.Service.Ioc;

namespace Baz.Caja.Servicios
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]

    public class CjCRNegocioService : CjCRServiceIoC<CjCRNegocioServiceIoC>, CjCRINegocioService
    {
        public List<CjCRUnidadNegocio> GetUnidadesNegocio()
        {
            return Service.GetUnidadesNegocio();
        }
    }
}
