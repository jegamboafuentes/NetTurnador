using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Baz.Caja.Commons.Core;
using Baz.Caja.Commons.Support.Test;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Support;

namespace Baz.Caja.Turnador.Test.Support
{
    public class CjCTEmpleadoPoolXmlMapper : CjCRIMapper<DataRow, CjCREmpleadoPool>
    {
        public const int BASIC_MODEL = 0;

        public CjCREmpleadoPoolFactory EmpleadoPoolFactory { get; set; }

        public CjCREmpleadoPool Get(DataRow row, int model)
        {
            switch (model)
            {
                case BASIC_MODEL:
                    return GetBasicModel(row);
                default:
                    throw new ArgumentException("Modelo no establecido");
            }
        }

        private CjCREmpleadoPool GetBasicModel(DataRow row)
        {
            CjCRGenericDataRowMapper mapper = new CjCRGenericDataRowMapper(row);
            CjCREmpleadoPool empleadoPool = EmpleadoPoolFactory.Get();

            empleadoPool.NoEmpleado = mapper.GetString("noEmpleado");
            empleadoPool.Estado = mapper.GetInt32("estado");

            foreach (CjCRGenericDataRowMapper subelement in 
                mapper.GetChildren("entrada_cualidades_cualidad"))
            {
                empleadoPool.Cualidades.Add(
                    subelement.GetInt32("idCualidad"),
                    subelement.GetString("valor"));
            }

            return empleadoPool;
        }
    }
}
