using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Validation;
//using Baz.Caja.Servicios.Entidades;
//using Baz.Caja.Servicios.Logica;

namespace Baz.Caja.Turnador.Support
{
    public class CjCRCredentialExtract
    {
        public CjCRAppConfig AppConfig { get; set; }
        public CjCRResourceCredentialValidator ResourceCredentialValidator { get; set; }
        //public ControladorServicios ControladorServicios { get; set; }

        public CjCRServiceToken GetServiceToken(String json)
        {
            CjCRResourceCredential resource = JsonConvert.DeserializeObject<CjCRResourceCredential>(json);
            CjCRSummary summary = ResourceCredentialValidator.Validate(resource,
                CjCRResourceCredentialValidator.BASIC_VALIDATION);
            CjCRServiceToken serviceToken = null;

            if (summary.Status == CjCRTurnadorStatus.DEFAULT_CREDENTIAL)
            {
                return GetDefaultServiceToken();
            }

            if (summary.Complete)
            {
                serviceToken = new CjCRServiceToken();

                //serviceToken.NumeroEmpleado = ControladorServicios.Descifra(
                //    resource.EM, resource.SU);
                //serviceToken.PasswordEmpleado = ControladorServicios.Descifra(
                //    resource.PW, resource.SU);
                //serviceToken.Token = resource.TK;
                serviceToken.Authorization = true;
            }

            return serviceToken;
        }

        //public CjCRCredential GetCredential(Empleado empleado)
        //{
        //    CjCRCredential credential = new CjCRCredential();

        //    credential.User = empleado.Numero;
        //    credential.Password = empleado.Password;

        //    return credential;
        //}

        public CjCRCredential GetCredential(CjCRServiceToken serviceToken)
        {
            CjCRCredential credential = new CjCRCredential();

            credential.User = serviceToken.NumeroEmpleado;
            credential.Password = serviceToken.PasswordEmpleado;

            return credential;
        }

        private CjCRServiceToken GetDefaultServiceToken()
        {
            CjCRServiceToken serviceToken = new CjCRServiceToken();

            serviceToken.Authorization = false;

            return serviceToken;
        }
    }

    public class CjCRResourceCredential
    {
        [JsonProperty(PropertyName = "em")]
        public String EM { get; set; }

        [JsonProperty(PropertyName = "pw")]
        public String PW { get; set; }

        [JsonProperty(PropertyName = "su")]
        public int SU { get; set; }

        [JsonProperty(PropertyName = "tk")]
        public String TK { get; set; }

        [JsonProperty(PropertyName = "dt")]
        public bool DT { get; set; }
    }
}