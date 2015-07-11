using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

using Baz.Caja.Commons.Core;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Support;

namespace Baz.Caja.Turnador.Dao.Mapper
{
    public class CjCREmpleadoPoolMapper : CjCRIMapper<DbDataReader, CjCREmpleadoPool>
    {
        public const int BASIC_MODEL = 0;

        public CjCREmpleadoPoolFactory EmpleadoPoolFactory { get; set; }

        public CjCREmpleadoPool Get(DbDataReader reader, int model)
        {
            switch (model)
            {
                case BASIC_MODEL:
                    return GetBasicModel(reader);
                default:
                    throw new ArgumentException(
                        CjCRPropertyUtils.Get("incorrect.model"));
            }
        }

        private CjCREmpleadoPool GetBasicModel(DbDataReader reader)
        {
            CjCREmpleadoPool empleadoPool = EmpleadoPoolFactory.Get(
                CjCRDaoUtils.ToString(reader, 0, "").Trim(),
                CjCRDaoUtils.ToInt16(reader, 2));

            empleadoPool.Foto = CjCRDaoUtils.ToString(reader, 1, "").Trim();

            empleadoPool.Cualidades.Add(
                CjCRDaoUtils.ToInt16(reader, 3),
                CjCRDaoUtils.ToString(reader, 4, "").Trim());

            return empleadoPool;
        }
    }
}
