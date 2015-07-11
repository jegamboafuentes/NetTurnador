using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Baz.Caja.Commons.Model;
using Baz.Caja.Turnador.Dao;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Service;
using Baz.Caja.Turnador.Support;
using Baz.Caja.Turnador.Test.Model;

namespace Baz.Caja.Turnador.Test.Logic
{
    public class CjCTTurnadorLogic
    {
        public CjCRCredential DefaultCredential { get; set; }
        public CjCTTestConfig TTestConfig { get; set; }
        public CjCRTurnosService TurnosService { get; set; }        
        public CjCRPoolService PoolService { get; set; }
        public CjCRTurnosDao TurnosDao { get; set; }
        public CjCRPoolDao PoolDao { get; set; }
        
        public CjCRTTurnoStatus PrepareTurno(CjCRTurno turno)
        {
            CjCRTTurnoStatus turnoStatus;
            bool bResult = true;

            turnoStatus = CreateTurno(turno);
            bResult = (bResult && turnoStatus.Status);
            
            bResult = (bResult && SetDisponible(turno.IdUnidadNegocio, 
                turno.Empleado.NoEmpleado));

            Delay();

            turnoStatus = GetTurno(turnoStatus.Turno);
            turnoStatus.Status = (bResult && turnoStatus.Status);

            return turnoStatus;
        }

        public bool SetDisponible(int idUnidadNegocio, String noEmpleado)
        {
            CjCREmpleadoPool empleadoPool = new CjCREmpleadoPool();
            bool bResult = true;
            CjCRSummary summary;

            summary = PoolService.SetNoDisponible(noEmpleado,
                DefaultCredential);
            bResult = (bResult && summary.Complete);

            summary = PoolService.SetDisponible(noEmpleado,
                DefaultCredential);
            bResult = (bResult && summary.Complete);

            return bResult;
        }

        public CjCRTTurnoStatus CreateTurno(CjCRTurno turno)
        {
            CjCRTTurnoStatus turnoStatus = new CjCRTTurnoStatus();
            bool bResult = true;

            turno = TurnosService.GenerarTurno(turno, DefaultCredential);
            bResult = (bResult && (turno.Fecha > 0));
            
            turnoStatus.Turno = turno;
            turnoStatus.Status = bResult;

            return turnoStatus;
        }

        public CjCRTTurnoStatusList CreateTurnos(CjCRTurno turno, int no)
        {
            CjCRTTurnoStatusList turnosStatus = new CjCRTTurnoStatusList();

            for (int i = 0; i < no; i++)
            {
                turnosStatus.AddElement(CreateTurno(turno));
                
            }

            return turnosStatus;
        }

        public CjCRTTurnoStatus GetTurnoEstado(CjCRTurno turno, int estado)
        {
            CjCRTTurnoStatus turnoStatus;
            bool bResult = true;

            turnoStatus = CreateTurno(turno);
            bResult = (bResult && turnoStatus.Status);

            turnoStatus = SetTurnoEstado(turnoStatus.Turno, estado);
            bResult = (bResult && turnoStatus.Status);

            turnoStatus.Status = bResult;

            return turnoStatus;
        }

        public CjCRTTurnoStatusList GetTurnosEstado(CjCRTurno turno, int estado, int no)
        {
            CjCRTTurnoStatusList turnosStatus = new CjCRTTurnoStatusList();

            for (int i = 0; i < no; i++)
            {
                turnosStatus.AddElement(GetTurnoEstado(turno, estado));
            }

            return turnosStatus;
        }

        public CjCRTTurnoStatus SetTurnoEstado(CjCRTurno turno, int estado)
        {
            CjCRTTurnoStatus turnoStatus;
            bool bResult = true;
            int result;

            turno.Estado = estado;

            result = TurnosDao.SetEstado(turno, DefaultCredential);
            bResult = (bResult && (result == CjCRTurnadorStatus.OPERATION_COMPLETE));

            turnoStatus = GetTurno(turno);
            bResult = turnoStatus.Status;

            turnoStatus.Turno = turno;
            turnoStatus.Status = bResult;

            return turnoStatus;
        }

        public CjCRTTurnoStatusList SetTurnosEstado(List<CjCRTurno> turnos, int estado)
        {
            CjCRTTurnoStatusList turnosStatus = new CjCRTTurnoStatusList();

            foreach (CjCRTurno turno in turnos)
            {
                turnosStatus.AddElement(SetTurnoEstado(turno, estado));
            }

            return turnosStatus;
        }

        public bool AtenderFinalizarTurno(CjCRTurno turno)
        {
            CjCRSummary summary = TurnosService.AtenderTurno(turno.Empleado.NoEmpleado, 
                turno, DefaultCredential);
            bool bResult = summary.Complete;

            return FinalizarTurno(turno);
        }

        public bool FinalizarTurno(CjCRTurno turno)
        {
            CjCRSummary summary;
            CjCRTTurnoStatus turnoStatus;
            bool bResult = true;

            turnoStatus = GetTurno(turno);
            turno = turnoStatus.Turno;
            bResult = turnoStatus.Status;

            summary = TurnosService.FinalizarTurno(turno.Empleado.NoEmpleado, 
                turno, DefaultCredential);
            bResult = (bResult && summary.Complete);

            return bResult;
        }

        public CjCRTTurnoStatus GetTurno(CjCRTurno turno)
        {
            CjCRTTurnoStatus turnoStatus = new CjCRTTurnoStatus();
            
            turnoStatus.Turno = TurnosService.GetTurno(turno.Fecha.ToString(), turno.IdUnidadNegocio.ToString(),
                turno.IdTurno.ToString(), DefaultCredential);
            turnoStatus.Status = (turnoStatus.Turno != default(CjCRTurno));

            return turnoStatus;
        }

        public void Delay()
        {
            Thread.Sleep(TTestConfig.TurnoDelay);
        }
    }

    public class CjCRTTurnoStatus
    {
        public CjCRTurno Turno { get; set; }
        public bool Status { get; set; }
    }

    public class CjCRTTurnoStatusList : List<CjCRTTurnoStatus>
    {
        public bool Status { get; set; }

        private List<CjCRTurno> Turnos;

        public CjCRTTurnoStatusList()
        {
            Turnos = new List<CjCRTurno>();
            Status = true;
        }

        public void AddElement(CjCRTTurnoStatus turnoStatus)
        {
            Status = (Status && turnoStatus.Status);

            Turnos.Add(turnoStatus.Turno);
            Add(turnoStatus);
        }

        public List<CjCRTurno> GetTurnos()
        {
            return Turnos;
        }
    }
}
