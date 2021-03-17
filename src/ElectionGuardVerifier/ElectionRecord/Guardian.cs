using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ElectionGuardVerifier.ElectionRecord
{
    public class Guardian
    {
        public string Id { get; set; }
        public List<GuardianProof> GuardianProofs { get; set; }
        /// <summary>
        /// K_i,0
        /// </summary>
        public BigInteger PublicKey_Ki { get; set; }
    }

    public class GuardianProof
    {
        public string Id { get; set; }
        /// <summary>
        /// K_ij
        /// </summary>
        public BigInteger PublicKey_K { get; set; }

        public BigInteger Commitment_h { get; set; }

        public BigInteger Challenge_c { get; set; }

        public BigInteger Response_u { get; set; }
    }
}
