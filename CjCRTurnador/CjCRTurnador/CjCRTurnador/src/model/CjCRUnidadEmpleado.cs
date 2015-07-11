using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


using Baz.Caja.Turnador.Util;

namespace Baz.Caja.Turnador.Model
{
    [DataContract]
    public class CjCRUnidadEmpleado
    {
        [DataMember]
        public String NoEmpleado { get; set; }

        [DataMember]
        public int IdUnidadNegocio { get; set; }
    }
}