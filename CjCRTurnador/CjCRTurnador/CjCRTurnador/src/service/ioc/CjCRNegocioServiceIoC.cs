using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Web;

using Baz.Caja.Servicios;
using Baz.Caja.Turnador.Model;

namespace Baz.Caja.Turnador.Service.Ioc
{
    public class CjCRNegocioServiceIoC : CjCRINegocioService
    {   
        public CjCRNegocioService NegocioService { get; set; }

        public List<CjCRUnidadNegocio> GetUnidadesNegocio()
        {
            return NegocioService.GetUnidadesNegocio(
                NegocioService.GetDefaultAuthorization(true));
        }
    }
}
