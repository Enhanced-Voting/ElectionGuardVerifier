using ElectionGuardVerifier.ElectionRecord;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ElectionGuardVerifier.Verifiers
{
    /// <summary>
    /// An election verifier must confirm the following for each guardian 𝑇𝑖 and for each 𝑗∈ℤ𝑘: 
    /// (A) The challenge 𝑐𝑖,𝑗 is correctly computed as 𝑐𝑖,𝑗=𝐻(𝑄,𝐾𝑖,𝑗,ℎ𝑖,𝑗) mod 𝑞. 
    /// (B) The equation 𝑔𝑢𝑖,𝑗 mod 𝑝=ℎ𝑖,𝑗𝐾𝑖,𝑗𝑐𝑖,𝑗 mod 𝑝 is satisfied.
    /// </summary>
    public class GuardianPublicKeyVerifier : Verifier
    {
        private List<VerificationStep> verificationSteps;
        private Guardian Guardian;
        public GuardianPublicKeyVerifier(BaselineParameters baselineParameters, ElectionParameters electionParameters, Guardian guardian) : base(baselineParameters, electionParameters)
        {
            verificationSteps = new List<VerificationStep>() {
            new VerificationStep(){
                Description= "Verify Guardian's Coefficient Proofs", 
                StepOrder = 1, 
                ErrorMessage="Challenge value (c_ij) for guardian does not match Hash(base hash, public key, commitment) mod q.", 
                SuccessMessage="Challenge value (c_ij) verified as Hash(base hash, public key, commitment) mod q.", 
                VerificationStepMethod = VerifyCoefficientProof 
            },
            new VerificationStep()
            {
                Description = "Verify Guardian's Public Key Equation",
                StepOrder = 2,
                ErrorMessage = "Equation of generator ^ response mod p = (commitment * public key ^ challenge) mod p did not equal.",
                SuccessMessage = "Equation of generator ^ response mod p = (commitment * public key ^ challenge) mod p is satisfied.",
                VerificationStepMethod = VerifyGeneratorEquation
            }
            };
            Guardian = guardian;
        }
        protected override string Description { get => "Guardian Public-Key Validation"; }
        protected override List<VerificationStep> VerificationSteps { get => verificationSteps; }
        
        private List<VerificationResultMessage> VerifyCoefficientProof()
        {
            List<VerificationResultMessage> resultMessages = new List<VerificationResultMessage>();

            foreach (var proof in Guardian.GuardianProofs)
            {
                var hashValue = Calculator.Hash(ElectionParameters.BaseHashValue_Q, proof.PublicKey_K, proof.Commitment_h);
                var calculatedProof = hashValue % BaselineParameters.SmallPrime_q;

                resultMessages.Add(new VerificationResultMessage() { IsSuccessful = proof.Challenge_c.Equals(calculatedProof), Reference = "Guardian: " + Guardian.Id + ", Proof: " +  proof.Id });

            }

            return resultMessages;
        }

        private List<VerificationResultMessage> VerifyGeneratorEquation()
        {
            List<VerificationResultMessage> resultMessages = new List<VerificationResultMessage>();

            foreach (var proof in Guardian.GuardianProofs)
            {
                var generatorFunctionResult = BigInteger.ModPow(BaselineParameters.Generator_g, proof.Response_u, BaselineParameters.LargePrime_p);
                var commitmentFunctionResult = BigInteger.Multiply(proof.Commitment_h, BigInteger.ModPow(proof.PublicKey_K, proof.Challenge_c, BaselineParameters.LargePrime_p)) % BaselineParameters.LargePrime_p;

                resultMessages.Add(new VerificationResultMessage() { IsSuccessful = generatorFunctionResult.Equals(commitmentFunctionResult), Reference = "Guardian: " + Guardian.Id + ", Proof: " + proof.Id });

            }

            return resultMessages;
        }
    
    }
}
