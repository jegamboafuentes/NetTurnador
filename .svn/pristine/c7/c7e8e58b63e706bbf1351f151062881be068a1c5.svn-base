using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Dao;
using Baz.Caja.Turnador.Support;

namespace Baz.Caja.Turnador.Logic
{
    public class CjCRTurnosTask
    {
        public CjCRSummary CompletarTurnos(CjCRCredential credential)
        {
            CjCRCompletarTurnosTask task = GetCompletarTurnosTask(credential);

            return task.Execute();
        }

        public void CompletarTurnosProcess(CjCRCredential credential)
        {
            CjCRCompletarTurnosTask task = GetCompletarTurnosTask(credential);
            Thread thread = new Thread(new ThreadStart(task.Call));

            thread.Start();
        }

        public CjCRSummary CaducarTurnos(int idUnidadNegocio, CjCRCredential credential)
        {
            CjCRCaducarTurnosTask task = GetCaducarTurnosTask(idUnidadNegocio, credential);

            return task.Execute();
        }

        public void CaducarTurnosProcess(int idUnidadNegocio, CjCRCredential credential)
        {
            CjCRCaducarTurnosTask task = GetCaducarTurnosTask(idUnidadNegocio, credential);
            Thread thread = new Thread(new ThreadStart(task.Call));

            thread.Start();
        }

        private CjCRCompletarTurnosTask GetCompletarTurnosTask(CjCRCredential credential)
        {
            return CjCRSpringContext.Get<CjCRCompletarTurnosTask>(
                "CompletarTurnosTask", credential);
        }

        private CjCRCaducarTurnosTask GetCaducarTurnosTask(int idUnidadNegocio, CjCRCredential credential)
        {
            return CjCRSpringContext.Get<CjCRCaducarTurnosTask>(
                "CaducarTurnosTask", idUnidadNegocio, credential);
        }
    }

    public class CjCRCompletarTurnosTask
    {
        public CjCRTurnosDao TurnosDao { get; set; }
        public CjCRTurnadorSummaryFactory TurnadorSummaryFactory { get; set; }

        private CjCRCredential Credential;

        public CjCRCompletarTurnosTask(CjCRCredential credential)
        {
            this.Credential = credential;
        }

        public void Call()
        {
            Execute();
        }

        public CjCRSummary Execute()
        {
            int statusResult = TurnosDao.CompletarTurnos(Credential);

            return TurnadorSummaryFactory.Get(statusResult);
        }
    }

    public class CjCRCaducarTurnosTask
    {
        public CjCRTurnosDao TurnosDao { get; set; }
        public CjCRTurnadorSummaryFactory TurnadorSummaryFactory { get; set; }

        private CjCRCredential Credential;
        private int IdUnidadNegocio;

        public CjCRCaducarTurnosTask(int idUnidadNegocio, CjCRCredential credential)
        {
            this.IdUnidadNegocio = idUnidadNegocio;
            this.Credential = credential;
        }

        public void Call()
        {
            Execute();
        }

        public CjCRSummary Execute()
        {
            int statusResult = TurnosDao.CaducarTurnos(IdUnidadNegocio, Credential);

            return TurnadorSummaryFactory.Get(statusResult);
        }
    }
}