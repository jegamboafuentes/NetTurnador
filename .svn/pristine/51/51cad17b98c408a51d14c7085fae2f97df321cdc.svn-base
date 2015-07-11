using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Web;
using Baz.Caja.Turnador.Logic;
using Baz.Caja.Turnador.Model;

namespace Baz.Caja.Turnador.Service
{
    public class CjCRNegocioService : CjCRServiceBase
    {
        public CjCRNegocioLogic NegocioLogic { get; set; }

        public List<CjCRUnidadNegocio> GetUnidadesNegocio(CjCRCredential credential)
        {
            return NegocioLogic.GetUnidadesNegocio(credential);
        }
    }
}
