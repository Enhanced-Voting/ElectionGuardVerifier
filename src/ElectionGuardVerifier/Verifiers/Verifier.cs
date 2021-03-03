using ElectionGuardVerifier.ElectionRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ElectionGuardVerifier.Verifiers
{
    public abstract class Verifier
    {
        protected Calculator Calculator { get; set; }
        protected BaselineParameters BaselineParameters { get; set; }
        protected ElectionParameters ElectionParameters { get; set; }

        protected abstract string Description { get; }
        protected abstract List<VerificationStep> VerificationSteps { get; }        

        public Verifier(BaselineParameters baselineParameters, ElectionParameters electionParameters) {
            
            BaselineParameters = baselineParameters;
            ElectionParameters = electionParameters;

            Calculator = new Calculator(baselineParameters.LargePrime_p, baselineParameters.SmallPrime_q);
        }

        /// <summary>
        /// Use reflection to call the verification step method defined in the child class list of steps
        /// </summary>
        /// <returns></returns>
        public List<VerificationResult> RunAllVerificationSteps()
        {
            List<VerificationResult> results = new List<VerificationResult>();

            foreach (var step in VerificationSteps.OrderBy(vs=>vs.StepOrder))
            {
                List<VerificationResultMessage> resultMessages = step.VerificationStepMethod();
                results.AddRange(resultMessages.Select(rm=>new VerificationResult() { VerificationStep = step, IsSuccessful = rm.IsSuccessful, Reference = rm.Reference }));               
            }

            return results;
        }

    }
}
