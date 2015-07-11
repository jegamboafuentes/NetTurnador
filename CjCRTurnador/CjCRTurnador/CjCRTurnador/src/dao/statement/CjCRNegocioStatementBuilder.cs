using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baz.Caja.Turnador.Dao.Statement
{
    public class CjCRNegocioStatementBuilder
    {
        public String GetUnidadNegocioStatement()
        {
            StringBuilder statement = new StringBuilder();

            statement.Append(" SELECT fiunidadnegocioid, fcdescripcion, fcrutaimagen, ");
            statement.Append(" flprestamos, fccolor, fczona ");
            statement.Append(" FROM tccjcctrunidadnegocio ");
            statement.Append(" WHERE fiunidadnegocioid = @fiunidadnegocioid ");

            return statement.ToString();
        }
    }
}
