using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Baz.Caja.Turnador.Model;

namespace Baz.Caja.Turnador.Test.Model
{
    [DataContract]
    public class CjCTTurnoApropiado
    {
        [DataMember]
        public CjCRTurno Turno { get; set; }

        [DataMember]
        public String NoEmpleadoAlterno { get; set; }
    }
}
