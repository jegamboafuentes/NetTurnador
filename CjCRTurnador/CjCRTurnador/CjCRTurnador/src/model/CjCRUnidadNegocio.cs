using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;


namespace Baz.Caja.Turnador.Model
{
    [DataContract]
    public class CjCRUnidadNegocio
    {
        [DataMember]
        public int IdUnidadNegocio{ get; set; }

        [DataMember]
        public String Descripcion { get; set; }

        [DataMember]
        public String Zona { get; set; }

        [DataMember]
        public bool Prestamos { get; set; }

        [DataMember]
        public String UrlImagen { get; set; }

        [DataMember]
        public String Color { get; set; }

        [DataMember]
        public int Estatus { get; set; }
    }
}
