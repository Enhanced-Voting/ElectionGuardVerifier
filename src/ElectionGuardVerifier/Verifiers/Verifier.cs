using ElectionGuardVerifier.ElectionRecord;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ElectionGuardVerifier.Verifiers
{
    public abstract class Verifier
    {
        protected Calculator Calculator { get; set; }
        protected BaselineParameters BaselineParameters { get; set; }        
        
        protected abstract string Description { get; set; }
        protected abstract List<VerificationStep> VerificationSteps { get; set; }        

        public Verifier(BaselineParameters baselineParameters) {
            
            BaselineParameters = baselineParameters;

            Calculator = new Calculator(baselineParameters.LargePrime_p, baselineParameters.SmallPrime_q);
        }

        public abstract List<VerificationResult> RunAllVerificationSteps();

    }
}
