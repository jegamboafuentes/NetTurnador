using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Baz.Caja.Commons.Core;
using Baz.Caja.Commons.Support.Test;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Test.Model;

namespace Baz.Caja.Turnador.Test.Support
{
    public class CjCTTurnoApropiadoXmlMapper : CjCRIMapper<DataRow, CjCTTurnoApropiado>
    {
        public const int BASIC_MODEL = 0;

        public CjCTEmpleadoPoolXmlMapper TEmpleadoPoolXmlMapper { get; set; }

        public CjCTTurnoApropiado Get(DataRow row, int model) 
        {
            switch (model)
            {
                case BASIC_MODEL:
                    return GetBasicModel(row);
                default:
                    throw new ArgumentException("Modelo no establecido");
            }
        }

        private CjCTTurnoApropiado GetBasicModel(DataRow row)
        {
            CjCRGenericDataRowMapper mapper = new CjCRGenericDataRowMapper(row);
            CjCTTurnoApropiado turnoApropiado = new CjCTTurnoApropiado();
            CjCRTurno turno = new CjCRTurno();
            CjCREmpleadoPool empleadoPool = new CjCREmpleadoPool();

            turno.IdOrigen = mapper.GetInt32("idOrigen");
            turno.IdUnidadNegocio = mapper.GetInt32("idUnidadNegocio");

            empleadoPool.NoEmpleado = mapper.GetString("noEmpleado");

            turno.Empleado = empleadoPool;

            turnoApropiado.Turno = turno;
            turnoApropiado.NoEmpleadoAlterno = mapper.GetString("noEmpleadoAlterno");

            return turnoApropiado;
        }
    }
}
