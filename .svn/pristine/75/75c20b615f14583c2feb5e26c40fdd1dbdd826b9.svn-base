using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Baz.Caja.Commons.Core;
using Baz.Caja.Commons.Model;
using Baz.Caja.Commons.Util;
using Baz.Caja.Turnador.Support;

namespace Baz.Caja.Turnador.Validation
{
    public class CjCRResourceCredentialValidator : CjCRIValidatorModel<CjCRResourceCredential, CjCRSummary>
    {
        public const int BASIC_VALIDATION = 0;

        public CjCRTurnadorSummaryFactory TurnadorSummaryFactory { get; set; }

        public CjCRSummary Validate(CjCRResourceCredential resource, int model)
        {
            if (CjCRAssertUtils.Empty(resource))
            {
                return TurnadorSummaryFactory.Get(CjCRTurnadorStatus.OPERATION_FAIL,
                    CjCRPropertyUtils.Get("resource.null"));
            }

            switch (model)
            {
                case BASIC_VALIDATION:
                    return BasicValidation(resource);
                default:
                    throw new ArgumentException(
                        CjCRPropertyUtils.Get("incorrect.model"));
            }
        }

        private CjCRSummary BasicValidation(CjCRResourceCredential resource)
        {
            List<String> invalidos = new List<String>();
            CjCRSummary summary;

            if (CjCRAssertUtils.Empty(resource.EM))
            {
                invalidos.Add("EM");
            }

            if (CjCRAssertUtils.Empty(resource.PW))
            {
                invalidos.Add("PW");
            }

            if (resource.SU < 1)
            {
                invalidos.Add("SU");
            }

            if (CjCRAssertUtils.Empty(resource.TK))
            {
                invalidos.Add("TK");
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

            if (!summary.Complete && resource.DT)
            {
                summary.Status = CjCRTurnadorStatus.DEFAULT_CREDENTIAL;
            }

            return summary;
        }
    }
}