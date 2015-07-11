using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Baz.Caja.Turnador.Model;

namespace Baz.Caja.Turnador.Support
{
    public class CjCREmpleadoPoolFactory
    {
        public CjCREmpleadoPool Get(String noEmpleado, int estado)
        {
            CjCREmpleadoPool empleadoPool = Get();

            empleadoPool.NoEmpleado = noEmpleado;
            empleadoPool.Estado = estado;

            return empleadoPool;
        }

        public CjCREmpleadoPool Get()
        {
            CjCREmpleadoPool empleadoPool = new CjCREmpleadoPool();

            return empleadoPool;
        }
    }
}