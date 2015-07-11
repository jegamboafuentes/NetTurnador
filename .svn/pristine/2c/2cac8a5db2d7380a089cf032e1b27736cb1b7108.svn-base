using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Baz.Caja.Commons.Core;
using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Model;
using Baz.Caja.Turnador.Support;

namespace Baz.Caja.Turnador.Validation
{
    public class CjCRTurnoValidator : CjCRIValidatorModel<CjCRTurno, CjCRSummary>
    {
        public const int BASIC_VALIDATION = 0;
        public const int ESTADO_VALIDATION = 1;

        public CjCRTurnadorSummaryFactory TurnadorSummaryFactory { get; set; }

        public CjCRSummary Validate(CjCRTurno turno, int model)
        {
            if (CjCRAssertUtils.Empty(turno))
            {
                return TurnadorSummaryFactory.Get(CjCRTurnadorStatus.OPERATION_FAIL,
                    CjCRPropertyUtils.Get("resource.null"));
            }

            switch (model)
            {
                case BASIC_VALIDATION:
                    return BasicValidation(turno);
                case ESTADO_VALIDATION:
                    return EstadoValidation(turno);
                default:
                    throw new ArgumentException(
                        CjCRPropertyUtils.Get("incorrect.model"));
            }
        }

        private CjCRSummary BasicValidation(CjCRTurno turno)
        {
            List<String> invalidos = new List<String>();
            CjCRSummary summary;

            if (turno.IdOrigen < 1)
            {
                invalidos.Add("IdOrigen");
            }

            if (turno.IdUnidadNegocio < 1)
            {
                invalidos.Add("IdUnidadNegocio");
            }

            if (invalidos.Count == 0)
            {
                summary = TurnadorSummaryFactory.Get(CjCRTurnadorStatus.OPERATION_COMPLETE);
            }
            else
            {
                summary = TurnadorSummaryFactory.Get(CjCRTurnadorStatus.INCOMPLETE_OPERATION,
                    String.Format("[{0}]", String.Join(",", invalidos.ToArray())));
            }

            return summary;
        }

        private CjCRSummary EstadoValidation(CjCRTurno turno)
        {
            List<String> invalidos = new List<String>();
            DateTime fecha = CjCRParseUtils.ToDateTime(turno.Fecha.ToString(), "yyyyMMdd");
            CjCRSummary summary;

            if (turno.IdTurno < 1)
            {
                invalidos.Add("IdTurno");
            }

            if (turno.IdUnidadNegocio < 1)
            {
                invalidos.Add("IdUnidadNegocio");
            }

            if (fecha.CompareTo(DateTime.Today) != 0)
            {
                invalidos.Add("Fecha");
            }

            if (invalidos.Count == 0)
            {
                summary = TurnadorSummaryFactory.Get(CjCRTurnadorStatus.OPERATION_COMPLETE);
            }
            else
            {
                summary = TurnadorSummaryFactory.Get(CjCRTurnadorStatus.INCOMPLETE_OPERATION,
                    String.Format("[{0}]", String.Join(",", invalidos.ToArray())));
            }

            return summary;
        }
    }
}