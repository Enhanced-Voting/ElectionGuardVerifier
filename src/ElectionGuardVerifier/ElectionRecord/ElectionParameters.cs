using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ElectionGuardVerifier.ElectionRecord
{
    public class ElectionParameters
    {
        public DateTime ElectionDate { get; set; }

        public string Location { get; set; }

        public int NumGardians_n { get; set; }

        public int NumQuorumGardians_k { get; set; }

        public BigInteger BaseHashValue_Q { get; set; }

        public BigInteger ExtendedHashValue_Qp { get; set; }
    }
}
