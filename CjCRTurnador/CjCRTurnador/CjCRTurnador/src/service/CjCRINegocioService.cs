using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

using Baz.Caja.Turnador.Model;

namespace Baz.Caja.Turnador.Service
{
    [ServiceContract]
    public interface CjCRINegocioService
    {
        [OperationContract]
        [WebGet(
            UriTemplate = "negocio/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<CjCRUnidadNegocio> GetUnidadesNegocio();
    }
}
