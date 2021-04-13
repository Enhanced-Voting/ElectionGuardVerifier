using ElectionGuardVerifier.ElectionRecord;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionGuardVerifier.Verifiers
{
    public class SelectionEncryptionVerifier : Verifier
    {

        private readonly List<VerificationStep> verificationSteps;

        private readonly BallotSelection BallotSelection;
        protected override string Description { get => "Correctness of Selection Encryptions Validation"; }
        protected override List<VerificationStep> VerificationSteps { get => verificationSteps; }

        public SelectionEncryptionVerifier(BaselineParameters baselineParameters, ElectionParameters electionParameters, BallotSelection ballotSelection) : base(baselineParameters, electionParameters)
        {
            BallotSelection = ballotSelection;

            verificationSteps = new List<VerificationStep>() {
            new VerificationStep(){
                Description= "Verify Values in Zp",
                StepOrder = 1,
                ErrorMessage="The given values are not in set Zp",
                SuccessMessage="The given values are in set Zp",
                VerificationStepMethod = VerifySelectionValuesInZp
            },
            new VerificationStep()
            {
                Description = "Verify Challenge Value",
                StepOrder = 2,
                ErrorMessage = "The given challenge was not correctly computed.",
                SuccessMessage = "The challenge value was correctly computed.",
                VerificationStepMethod = VerifySelectionChallenge
            },
            new VerificationStep()
            {
                Description = "Verify Commitment Values in Zq",
                StepOrder = 3,
                ErrorMessage = "The given commitment values are not in set Zq",
                SuccessMessage = "The given commitment values are in set Zq",
                VerificationStepMethod = VerifyCommitmentValuesInZq
            },
            new VerificationStep()
            {
                Description = "Verify Sum of Commitments",
                StepOrder = 4,
                ErrorMessage = "The sum of C_1 and C_2 is not equal to C",
                SuccessMessage = "The sum of C_1 and C_2 is equal to C",
                VerificationStepMethod = VerifyChallengeValueEquation
            },
            new VerificationStep()
            {
                Description = "Verify Proof of Zero",
                StepOrder = 5,
                ErrorMessage = "The Chaum-Pedersen proof for zero did not validate",
                SuccessMessage = "The Chaum-Pedersen proof for zero validated",
                VerificationStepMethod = VerifyChaumPetersenZeroProof
            },
            new VerificationStep()
            {
                Description = "Verify Proof of One",
                StepOrder = 6,
                 ErrorMessage = "The Chaum-Pedersen proof for one did not validate",
                SuccessMessage = "The Chaum-Pedersen proof for one validated",
                VerificationStepMethod = VerifyChaumPetersenOneProof
            }
            };
        }

        private List<VerificationResultMessage> VerifySelectionValuesInZp()
        {
            List<VerificationResultMessage> resultMessages = new List<VerificationResultMessage>();

            resultMessages.Add(new VerificationResultMessage() { 
                IsSuccessful = Calculator.IsWithinZp(BallotSelection.Ciphertext.Alpha)
                            && Calculator.IsWithinZp(BallotSelection.Ciphertext.Beta)
                            && Calculator.IsWithinZp(BallotSelection.Proof.Proof_zero_commitment.Alpha)
                            && Calculator.IsWithinZp(BallotSelection.Proof.Proof_zero_commitment.Beta)
                            && Calculator.IsWithinZp(BallotSelection.Proof.Proof_one_commitment.Alpha)
                            && Calculator.IsWithinZp(BallotSelection.Proof.Proof_one_commitment.Beta)
                ,
                Reference = "Ballot Selection: " + BallotSelection.Id
            });

            return resultMessages;
        }

        private List<VerificationResultMessage> VerifySelectionChallenge()
        {
            List<VerificationResultMessage> resultMessages = new List<VerificationResultMessage>();

            resultMessages.Add(new VerificationResultMessage()
            {
                IsSuccessful = BallotSelection.Proof.Challenge.Equals(
                    Calculator.Hash(
                        ElectionParameters.ExtendedHashValue_Qp, 
                        BallotSelection.Ciphertext.Alpha,
                        BallotSelection.Ciphertext.Beta,
                        BallotSelection.Proof.Proof_zero_commitment.Alpha,
                        BallotSelection.Proof.Proof_zero_commitment.Beta,
                        BallotSelection.Proof.Proof_one_commitment.Alpha,
                        BallotSelection.Proof.Proof_one_commitment.Beta
                        ))
                ,Reference = "Ballot Selection: " + BallotSelection.Id
            });

            return resultMessages;
        }

        private List<VerificationResultMessage> VerifyCommitmentValuesInZq() {
            
            List<VerificationResultMessage> resultMessages = new List<VerificationResultMessage>();

            resultMessages.Add(new VerificationResultMessage()
            {
                IsSuccessful = Calculator.IsWithinZq(BallotSelection.Proof.Proof_zero_challenge_c_0)
                        && Calculator.IsWithinZq(BallotSelection.Proof.Proof_one_challenge_c_1)
                        && Calculator.IsWithinZq(BallotSelection.Proof.Proof_zero_response_v_0)
                        && Calculator.IsWithinZq(BallotSelection.Proof.Proof_one_response_v_1)
                ,Reference = "Ballot Selection: " + BallotSelection.Id
            });

            return resultMessages;
        }

        private List<VerificationResultMessage> VerifyChallengeValueEquation() {
            
            List<VerificationResultMessage> resultMessages = new List<VerificationResultMessage>();

            resultMessages.Add(new VerificationResultMessage()
            {
                IsSuccessful = BallotSelection.Proof.Challenge.Equals(
                    Calculator.Sum(
                        BallotSelection.Proof.Proof_zero_challenge_c_0, 
                        BallotSelection.Proof.Proof_one_challenge_c_1,
                        BaselineParameters.SmallPrime_q))
                ,Reference = "Ballot Selection: " + BallotSelection.Id
            });

            return resultMessages;
        }
        private List<VerificationResultMessage> VerifyChaumPetersenZeroProof()
        {
            List<VerificationResultMessage> resultMessages = new List<VerificationResultMessage>();

            resultMessages.Add(new VerificationResultMessage()
            {
                IsSuccessful = CheckChaumPedersenEquations(
                    BallotSelection.Ciphertext, 
                    BallotSelection.Proof.Proof_zero_commitment, 
                    BallotSelection.Proof.Proof_zero_challenge_c_0, 
                    BallotSelection.Proof.Proof_zero_response_v_0
                    )
               ,Reference = "Ballot Selection: " + BallotSelection.Id
            });

            return resultMessages;
        }
        private List<VerificationResultMessage> VerifyChaumPetersenOneProof()
        {
            List<VerificationResultMessage> resultMessages = new List<VerificationResultMessage>();

            resultMessages.Add(new VerificationResultMessage()
            {
                IsSuccessful = CheckChaumPedersenEquations(
                    BallotSelection.Ciphertext,
                    BallotSelection.Proof.Proof_one_commitment,
                    BallotSelection.Proof.Proof_one_challenge_c_1,
                    BallotSelection.Proof.Proof_one_response_v_1
                    )
               ,
                Reference = "Ballot Selection: " + BallotSelection.Id
            });


            return resultMessages;
        }

   
    }
}
