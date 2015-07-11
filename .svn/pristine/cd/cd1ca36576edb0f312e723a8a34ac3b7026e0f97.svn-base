using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Web;

using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Util;
using Baz.Caja.Commons.Web;
using Baz.Caja.Turnador.Support;
//using Baz.Caja.Servicios.Entidades;
//using Baz.Caja.Servicios.Logica;

namespace Baz.Caja.Turnador.Service
{
    public abstract class CjCRServiceBase
    {
        public CjCRAppConfig AppConfig { get; set; }
        public CjCRCredentialExtract CredentialExtract { get; set; }
        //public ControladorServicios ControladorServicios { get; set; }

        public CjCRCredential GetDefaultAuthorization()
        {
            return GetDefaultAuthorization(false);
        }

        public CjCRCredential GetDefaultAuthorization(bool crossDomain)
        {
            if (crossDomain)
            {
                SetHeaders();
            }

            return CjCRSpringContext.Get<CjCRCredential>("DefaultCredential");
        }

        public CjCRCredential GetAuthorization()
        {
            return GetAuthorization(false);
        }

        public CjCRCredential GetAuthorization(bool crossDomain)
        {
            WebOperationContext context = WebOperationContext.Current;
            String json = context.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            CjCRServiceToken serviceToken = CredentialExtract.GetServiceToken(json);
            //Empleado empleado = null;

            if (serviceToken != null)
            {
                if (!serviceToken.Authorization)
                {
                    return GetDefaultAuthorization(crossDomain);
                }
                else
                {
                    //empleado = ControladorServicios.ObtieneSesionValida(
                    //    serviceToken.NumeroEmpleado, serviceToken.PasswordEmpleado,
                    //    serviceToken.Token);
                }
            }

            //if (empleado == null)
            //{
            //    throw new InvalidOperationException(
            //        CjCRPropertyUtils.Get("invalid.authorization"));
            //}

            if (crossDomain)
            {
                SetHeaders();
            }

            return CredentialExtract.GetCredential(serviceToken);
        }

        public void SetHeaders()
        {
            WebOperationContext.Current.OutgoingResponse.Headers[
                CjCRHttpResponseHeaders.AccessControlAllowOrigin] = "*";
            WebOperationContext.Current.OutgoingResponse.Headers[
                CjCRHttpResponseHeaders.CacheControl] = "no-cache";
        }
    }
}
