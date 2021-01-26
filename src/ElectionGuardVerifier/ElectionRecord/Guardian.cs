using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ElectionGuardVerifier.ElectionRecord
{
    public class Guardian
    {
        public BigInteger PublicKey_K { get; set; }

        public BigInteger Commitment_h { get; set; }

        public BigInteger Challenge_c { get; set; }

        public BigInteger Response_u { get; set; }
    }
}
