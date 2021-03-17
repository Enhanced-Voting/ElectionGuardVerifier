using ElectionGuardVerifier.ElectionRecord;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionGuardVerifier.Verifiers
{
    public class SelectionEncryptionVerifier : Verifier
    {

        private readonly List<VerificationStep> verificationSteps;
        protected override string Description { get => "Correctness of Selection Encryptions Validation"; }
        protected override List<VerificationStep> VerificationSteps { get => verificationSteps; }

        public SelectionEncryptionVerifier(BaselineParameters baselineParameters, ElectionParameters electionParameters) : base(baselineParameters, electionParameters)
        { 
        
        }
    }
}
