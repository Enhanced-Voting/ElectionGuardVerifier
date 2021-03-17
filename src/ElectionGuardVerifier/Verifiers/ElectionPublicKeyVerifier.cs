using ElectionGuardVerifier.ElectionRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ElectionGuardVerifier.Verifiers
{
    /// <summary>
    /// An election verifier must verify the correct computation of the joint election public key and extended base hash. 
    /// (A) 𝑄̅=𝐻(𝑄,𝐾1,0,𝐾1,1,𝐾1,2,…,𝐾1,𝑘−1,𝐾2,0,𝐾2,1,𝐾2,2,…,𝐾2,𝑘−1,…,𝐾𝑛,0,𝐾𝑛,1,𝐾𝑛,2,…,𝐾𝑛,𝑘−1) 
    /// (B) 𝐾=Π𝐾𝑖𝑛𝑖=1 mod 𝑝.
    /// </summary>
    public class ElectionPublicKeyVerifier : Verifier
    {
        private readonly List<Guardian> Guardians;
        private readonly List<VerificationStep> verificationSteps;
        protected override string Description { get => "Election Public-Key Validation"; }
        protected override List<VerificationStep> VerificationSteps { get => verificationSteps; }

        public ElectionPublicKeyVerifier(BaselineParameters baselineParameters, ElectionParameters electionParameters, List<Guardian> guardians) : base(baselineParameters, electionParameters)
        {
            verificationSteps = new List<VerificationStep>() {
            new VerificationStep(){
                Description= "Verify Extended Hash",
                StepOrder = 1,
                ErrorMessage="Extended hash (Ǭ) failed to verify as hash of public commitments (K_ij) together with the base hash (Q).",
                SuccessMessage="Extended hash (Ǭ) verified as hash of public commitments (K_ij) together with the base hash (Q).",
                VerificationStepMethod = VerifyExtendedHash
            },
            new VerificationStep()
            {
                Description = "Verify Aggregate Election Public Key",
                StepOrder = 2,
                ErrorMessage = "Aggregate election public key failed to verify as sum of guardian's public keys.",
                SuccessMessage = "Aggregate election public key verified as sum of guardian's public keys.",
                VerificationStepMethod = VerifyElectionPublicKey
            }
            };
            Guardians = guardians;
        }

        private List<VerificationResultMessage> VerifyExtendedHash()
        {
            List<VerificationResultMessage> resultMessages = new List<VerificationResultMessage>();
            List<BigInteger> toHash = new List<BigInteger>
            {
                ElectionParameters.BaseHashValue_Q
            };

            foreach (var guardian in Guardians)
            {
                toHash.AddRange(guardian.GuardianProofs.Select(gp => gp.PublicKey_K));                
            }

            var computedHash = Calculator.Hash(toHash);

            resultMessages.Add(new VerificationResultMessage() { IsSuccessful = computedHash.Equals(ElectionParameters.ExtendedHashValue_Qp)});

            return resultMessages;
        }

        private List<VerificationResultMessage> VerifyElectionPublicKey()
        {
            List<VerificationResultMessage> resultMessages = new List<VerificationResultMessage>();

            BigInteger aggregateKey = BigInteger.Zero;

            foreach (var guardian in Guardians)
            {
                BigInteger.Add(aggregateKey, guardian.PublicKey_Ki);
            }

            aggregateKey %= BaselineParameters.LargePrime_p;

            resultMessages.Add(new VerificationResultMessage() { IsSuccessful = aggregateKey.Equals(ElectionParameters.PublicKey_K) });

            return resultMessages;

        }


    }
}
