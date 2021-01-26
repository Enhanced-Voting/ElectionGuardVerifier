using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ElectionGuardVerifier.ElectionRecord
{
    public class BaselineParameters
    {
        private BigInteger largePrime_p;
        public BigInteger LargePrime_p {
            get { return largePrime_p; } 
            set {
                // check and throw error if not the expected value

                largePrime_p = value;
            }
        }

        private BigInteger smallPrime_q;
        public BigInteger SmallPrime_q
        {
            get { return smallPrime_q; }
            set
            {
                // check and throw error if not the expected value

                smallPrime_q = value;
            }
        }


        private BigInteger coFactor_r;
        public BigInteger CoFactor_r
        {
            get { return coFactor_r; }
            set
            {
                // check and throw error if not the expected value

                coFactor_r = value;
            }
        }

        private BigInteger generator_g;
        public BigInteger Generator_g
        {
            get { return generator_g; }
            set
            {
                // check and throw error if not the expected value

                generator_g = value;
            }
        }
    }
}
