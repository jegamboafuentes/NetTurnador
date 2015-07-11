using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Baz.Caja.Turnador.Test.Model
{
    [DataContract]
    public class CjCTTestConfig
    {
        [DataMember]
        public int TurnoDelay { get; set; }
    }
}
