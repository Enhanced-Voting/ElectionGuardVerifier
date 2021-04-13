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

        /// <summary>
        /// Calculate to see if the two Chaum-Pedersen functions are satisfied with the given inputs
        /// </summary>
        /// <param name="encryption"></param>
        /// <param name="commitment"></param>
        /// <param name="challenge"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected bool CheckChaumPedersenEquations(CipherPair encryption, CipherPair commitment, BigInteger challenge, BigInteger response) {

            
            BigInteger left_1 = Calculator.Exponentiate(BaselineParameters.Generator_g, response, BaselineParameters.LargePrime_p);
            BigInteger right_1 = Calculator.Multiply(
                commitment.Alpha,
                Calculator.Exponentiate(encryption.Alpha, challenge, BaselineParameters.LargePrime_p),
                BaselineParameters.LargePrime_p
                );

            BigInteger left_2 = Calculator.Exponentiate(ElectionParameters.PublicKey_K, response, BaselineParameters.LargePrime_p);
            BigInteger right_2 = Calculator.Multiply(
                commitment.Beta,
                Calculator.Exponentiate(encryption.Beta, challenge, BaselineParameters.LargePrime_p),
                BaselineParameters.LargePrime_p
                );

            return left_1.Equals(right_1) && left_2.Equals(right_2);
        
        }
    }
}
