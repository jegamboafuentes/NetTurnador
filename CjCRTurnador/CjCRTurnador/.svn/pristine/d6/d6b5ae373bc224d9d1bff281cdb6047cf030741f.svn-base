using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

using Baz.Caja.Commons.Core;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Model;

namespace Baz.Caja.Turnador.Dao.Mapper
{
    public class CjCRUnidadNegocioMapper : CjCRIMapper<DbDataReader, CjCRUnidadNegocio>
    {
        public const int BASIC_MODEL = 0;

        public CjCRUnidadNegocio Get(DbDataReader reader, int model)
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

        private CjCRUnidadNegocio GetBasicModel(DbDataReader reader)
        {
            CjCRUnidadNegocio unidadNegocio = new CjCRUnidadNegocio();

            unidadNegocio.IdUnidadNegocio = CjCRDaoUtils.ToInt16(reader, 0, 0); 
            unidadNegocio.Descripcion = CjCRDaoUtils.ToString(reader, 1, "").Trim();
            unidadNegocio.UrlImagen = CjCRDaoUtils.ToString(reader, 2, "").Trim();
            unidadNegocio.Prestamos = (CjCRDaoUtils.ToInt32(reader, 3) == 1 ? true : false);
            unidadNegocio.Color = CjCRDaoUtils.ToString(reader, 4, "").Trim();
            unidadNegocio.Zona = CjCRDaoUtils.ToString(reader, 5, "").Trim();
            unidadNegocio.Estatus = CjCRDaoUtils.ToInt16(reader, 7, 0);

            return unidadNegocio;
        }
    }
}
