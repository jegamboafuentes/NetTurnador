using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Baz.Caja.Commons.Collection;
using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Support;
using Baz.Caja.Turnador.Dao;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Support;
using Baz.Caja.Turnador.Logic;
using Baz.Caja.Turnador.Util;

namespace Baz.Caja.Turnador.Logic
{
    public class CjCRPoolLogic
    {
        public CjCRTurnosLogic TurnosLogic { get; set; }
        public CjCRTurnosTask TurnosTask { get; set; }
        public CjCRPoolDao PoolDao { get; set; }
        public CjCRStateMachinePool EstadosAtencionPool { get; set; }
        public CjCRTurnadorSummaryFactory TurnadorSummaryFactory { get; set; }
        public CjCRNegocioDao NegocioDao { get; set; }

        public CjCRSummary SetDisponible(String noEmpleado, 
            CjCRCredential credential)
        {
            String origen = "SetDisponible";
            CjCREmpleadoPool empleadoPool = PoolDao.GetPoolByEmpleado(noEmpleado, credential);
            CjCRCualidadList cualidad = new CjCRCualidadList();
            int statusResult = CjCRTurnadorStatus.OPERATION_FAIL;
            int unidadNegocio = PoolDao.UnidadEmpleado(noEmpleado, credential);
            empleadoPool.NoEmpleado = noEmpleado;
            CjCRSummary summary;
            cualidad.SetIdUnidadNegocio(unidadNegocio);
            empleadoPool.Cualidades = cualidad;

            if (empleadoPool != default(CjCREmpleadoPool))
            {
                //empleadoPool.Cualidades.SetIdUnidadNegocio(idUnidadNegocio);

                if (EstadosAtencionPool.IsNext(empleadoPool.Estado, 
                    CjCRTurnadorStatus.ATENCION_DISPONIBLE))
                {
                    empleadoPool.Estado = CjCRTurnadorStatus.ATENCION_DISPONIBLE;

                    statusResult = PoolDao.SetEstado(empleadoPool, origen, credential);
                }
            }

            summary = TurnadorSummaryFactory.Get(statusResult);

            if (summary.Complete)
            {
                TurnosTask.CompletarTurnosProcess(credential);
            }

            return summary;
        }

        public CjCRSummary SetNoDisponible(String noEmpleado,
            CjCRCredential credential)
        {
            String origen = "SetNoDisponible";
            CjCREmpleadoPool empleadoPool = new CjCREmpleadoPool();
            empleadoPool = PoolDao.GetPoolByEmpleado(noEmpleado, credential); ;
            CjCRCualidadList cualidad = new CjCRCualidadList();
            int statusResult;
            int unidadNegocio = PoolDao.UnidadEmpleado(noEmpleado, credential);
            //empleadoPool.Estado = 0;
            if (empleadoPool != null)
            {
                cualidad.SetIdUnidadNegocio(unidadNegocio);
                empleadoPool.Cualidades = cualidad;


                if (empleadoPool.Estado == CjCRTurnadorStatus.ATENCION_ASIGNADO || empleadoPool.Estado == CjCRTurnadorStatus.ATENCION_OCUPADO)
                {
                    statusResult = CjCRTurnadorStatus.EMPLEADO_ESTADO_INCORRECTO;
                }
                else
                {
                    empleadoPool.Estado = CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE;
                    statusResult = PoolDao.SetEstado(empleadoPool,origen, credential);
                }


            }
            else
            {
                CjCREmpleadoPool empleadoPool1 = new CjCREmpleadoPool();
                empleadoPool1.Estado = CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE;
                empleadoPool1.NoEmpleado = noEmpleado;
                cualidad.SetIdUnidadNegocio(unidadNegocio);
                empleadoPool1.Cualidades = cualidad;                
                statusResult = PoolDao.SetEstado(empleadoPool1, origen, credential);
            }
            return TurnadorSummaryFactory.Get(statusResult);

        }


        public CjCRUnidadEmpleado GetUnidadEmpleado(String noEmpleado, CjCRCredential credential)
        {

            
            CjCREmpleadoPool empPool = PoolDao.GetPoolByEmpleado(noEmpleado, credential);
            Int32 unidadNegocio;
            String numeroEmpleado;
            CjCRUnidadEmpleado unidadEmpleado = new CjCRUnidadEmpleado();
            if (empPool != default(CjCREmpleadoPool))
            {
                unidadNegocio = empPool.Cualidades.GetIdUnidadNegocio();
                numeroEmpleado = empPool.NoEmpleado;
                unidadEmpleado.NoEmpleado = numeroEmpleado;
                unidadEmpleado.IdUnidadNegocio = unidadNegocio;
            }
            return unidadEmpleado;
        }


        public CjCRSummary UpdateUnidadEmpleado(String noEmpleado, int unidadNegocio, CjCRCredential credential)

        {

            string origen = "UpdateUnidadEmpleado";
            CjCREmpleadoPool empPool = PoolDao.GetPoolByEmpleado(noEmpleado, credential);
            int status = CjCRTurnadorStatus.UNIDAD_NEGOCIO_INEXISTENTE;
            if (empPool != default(CjCREmpleadoPool))
            {
                int statemp = empPool.Estado;
                if (statemp == CjCRTurnadorStatus.ATENCION_NO_DISPONIBLE)
                {
                    CjCRDictionary<Int32, CjCRUnidadNegocio> GetUnidadNegocio = NegocioDao.GetUnidadesNegocio(credential);
                    foreach (int i in GetUnidadNegocio.Keys)
                    {
                        if (unidadNegocio == i)
                        {
                            status = PoolDao.UpdatePoolExecute(noEmpleado, unidadNegocio,origen, credential);
                            break;
                        }
                    }
                }
                else
                {
                    status = CjCRTurnadorStatus.EMPLEADO_ESTADO_INCORRECTO;
                }
            }
            else { status = CjCRTurnadorStatus.EMPLEADO_INEXISTENTE; }
            return TurnadorSummaryFactory.Get(status);
        }

        public CjCRSummary ConsultaCapacidad(String noEmpleado, CjCRCredential credential)
        {
            CjCREmpleadoPool empleado = PoolDao.GetPoolByEmpleado(noEmpleado, credential);
            int status = CjCRTurnadorStatus.UNIDAD_NEGOCIO_INACTIVA;
                CjCREmpleadoPool empleadoPool = new CjCREmpleadoPool();
                CjCRCualidadList cualidad = new CjCRCualidadList();
                CjCRSummary estado = new CjCRSummary();
                empleadoPool.NoEmpleado = noEmpleado;
                int unidadNegocioEmpleado = PoolDao.UnidadEmpleado(noEmpleado, credential);
                if (unidadNegocioEmpleado == CjCRTurnadorStatus.OPERATION_FAIL) { 
                    status =  CjCRTurnadorStatus.OPERATION_FAIL;
                    return TurnadorSummaryFactory.Get(status);
                }
                CjCRDictionary<Int32, CjCRUnidadNegocio> unidadesNegocio = NegocioDao.GetUnidadesNegocio(credential);
                List<CjCRUnidadNegocio> unidadesNegocioList = unidadesNegocio.Values.ToList();

                foreach (CjCRUnidadNegocio i in unidadesNegocioList)
                {
                    if (i.IdUnidadNegocio == unidadNegocioEmpleado)
                    {
                        if (i.Estatus == 1)
                        {
                            status = CjCRTurnadorStatus.OPERATION_COMPLETE;
                        }
                        else
                        {
                            status = CjCRTurnadorStatus.UNIDAD_NEGOCIO_INACTIVA;
                        }
                    }

                }
                return TurnadorSummaryFactory.Get(status);
            }

        public CjCRSummary CambiaPuntoAtencion(String noEmpleado, String puntoatencion, CjCRCredential credential) 
        {
            string origen = "PuntoAtencion";
            int status = CjCRTurnadorStatus.EMPLEADO_INEXISTENTE;
            status = PoolDao.PuntoAtencion(noEmpleado, puntoatencion,origen, credential);
            return TurnadorSummaryFactory.Get(status);
        }

        public CjCREmpleadoPool GetEmpleado(String noEmpleado, CjCRCredential credential)
        {
            CjCREmpleadoPool empPool = new CjCREmpleadoPool();
            empPool = PoolDao.GetPoolByEmpleado(noEmpleado,credential);
            if (empPool == null) empPool = new CjCREmpleadoPool();
            return empPool;
        }

        }

    }

