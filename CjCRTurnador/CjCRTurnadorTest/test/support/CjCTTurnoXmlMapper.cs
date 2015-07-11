using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Baz.Caja.Commons.Core;
using Baz.Caja.Commons.Support.Test;
using Baz.Caja.Turnador.Model;

namespace Baz.Caja.Turnador.Test.Support
{
    public class CjCTTurnoXmlMapper : CjCRIMapper<DataRow, CjCRTurno>
    {
        public const int BASIC_MODEL = 0;

        public CjCTEmpleadoPoolXmlMapper TEmpleadoPoolXmlMapper { get; set; }

        public CjCRTurno Get(DataRow row, int model) 
        {
            switch (model)
            {
                case BASIC_MODEL:
                    return GetBasicModel(row);
                default:
                    throw new ArgumentException("Modelo no establecido");
            }
        }

        private CjCRTurno GetBasicModel(DataRow row)
        {
            CjCRGenericDataRowMapper mapper = new CjCRGenericDataRowMapper(row);
            CjCRTurno turno = new CjCRTurno();
            CjCREmpleadoPool empleadoPool = new CjCREmpleadoPool();

            turno.IdOrigen = mapper.GetInt32("idOrigen");
            turno.IdUnidadNegocio = mapper.GetInt32("idUnidadNegocio");
            turno.Estado = mapper.GetInt32("estado");

            empleadoPool.NoEmpleado = mapper.GetString("noEmpleado");

            turno.Empleado = empleadoPool;

            return turno;
        }
    }
}
