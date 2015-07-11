﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;

using Baz.Caja.Commons.Core;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Model;

namespace Baz.Caja.Turnador.Dao.Mapper
{
    public class CjCRTurnoMapper : CjCRIMapper<DbDataReader, CjCRTurno>
    {
        public const int BASIC_MODEL = 0;

        public CjCRTurno Get(DbDataReader reader, int model)
        {
            switch (model)
            {
                case BASIC_MODEL:
                    return BasicModel(reader);
                default:
                    throw new ArgumentException(
                        CjCRPropertyUtils.Get("incorrect.model"));
            }
        }

        private CjCRTurno BasicModel(DbDataReader reader)
        {
            CjCRTurno turno = new CjCRTurno();
            CjCREmpleadoPool empleadoPool;
            String noEmpleado;
            String nombre;
            String apellidoPaterno;
            String apellidoMaterno;
            String puntoatencion;
            

            turno.Fecha = CjCRDaoUtils.ToInt32(reader, 0);
            turno.IdTurno = CjCRDaoUtils.ToInt32(reader, 1);
            turno.IdUnidadNegocio = CjCRDaoUtils.ToInt16(reader, 2);
            turno.Estado = CjCRDaoUtils.ToInt16(reader, 3);
            turno.Prioridad = CjCRDaoUtils.ToInt16(reader, 4);
            turno.IdOrigen = CjCRDaoUtils.ToInt32(reader, 5);
            turno.TurnoSeguimiento = CjCRDaoUtils.ToInt32(reader, 6);
            turno.Virtual = CjCRDaoUtils.ToInt16(reader, 12);

            noEmpleado = CjCRDaoUtils.ToString(reader, 7, "").Trim();
            nombre = CjCRDaoUtils.ToString(reader, 8, "").Trim();
            apellidoPaterno = CjCRDaoUtils.ToString(reader, 9, "").Trim();
            apellidoMaterno = CjCRDaoUtils.ToString(reader, 10, "").Trim();
            puntoatencion = CjCRDaoUtils.ToString(reader, 11, "").Trim();


            if (noEmpleado.Length > 0)
            {
                empleadoPool = new CjCREmpleadoPool();

                empleadoPool.NoEmpleado = noEmpleado;
                empleadoPool.Nombre = nombre;
                empleadoPool.PuntoAtencion = puntoatencion;

                turno.Empleado = empleadoPool;
            }

            return turno;
        }
    }
}
