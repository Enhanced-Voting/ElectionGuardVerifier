using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ElectionGuardVerifier.ElectionRecord
{
    public class BallotSelection
    {
        public string Id { get; set; }

        public CipherPair Ciphertext { get; set; }
        public BallotSelectionProof Proof { get; set; }

                
    }
    public class CipherPair
    {
        public BigInteger Alpha { get; set; }
        public BigInteger Beta { get; set; }
    }

    public class BallotSelectionProof
    {     

        public BigInteger Challenge { get; set; }
        public BigInteger Proof_one_challenge_c_1 { get; set; }
        public CipherPair Proof_one_commitment { get; set; }
        public BigInteger Proof_one_response_v_1 { get; set; }
        public BigInteger Proof_zero_challenge_c_0 { get; set; }
        public CipherPair Proof_zero_commitment { get; set; }
        public BigInteger Proof_zero_response_v_0 { get; set; }
    }
}
