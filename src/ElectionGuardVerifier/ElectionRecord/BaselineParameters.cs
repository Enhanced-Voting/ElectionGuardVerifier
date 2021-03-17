using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace ElectionGuardVerifier.ElectionRecord
{
    public class BaselineParameters
    {

        public BaselineParameters(BigInteger largePrime_p, BigInteger smallPrime_q, BigInteger coFactor_r, BigInteger generator_g)
        {
            if (!largePrime_p.Equals(ExpectedLargePrime))
                throw new Exception("Invalid baseline parameters: Large prime does not match expected large prime.");
            
            if(!smallPrime_q.Equals(ExpectedSmallPrime))
                throw new Exception("Invalid baseline parameters: Small prime does not match expected small prime.");

            if (!BigInteger.Equals(largePrime_p + BigInteger.MinusOne, BigInteger.Multiply(smallPrime_q, coFactor_r)))
                throw new Exception("Invalid baseline parameters: p - 1 does not equal r * q");

            // todo: validate the need for this
            if (smallPrime_q % coFactor_r == 0)
                throw new Exception("Invalid baseline parameters: Small prime, q, is divisible by cofactor, r. ");

            if (!BigInteger.Equals(generator_g, BigInteger.ModPow(2, coFactor_r, LargePrime_p)))
                throw new Exception("Invalid baseline parameters: Generator, g, is not equal to 2^r mod p");

            // todo: validate the need for this
            if (BigInteger.ModPow(generator_g, smallPrime_q, LargePrime_p) != 1)
                throw new Exception("Invalid baseline parameters: Generator, g^q mod p is not equal to 1.");

            // checks pass, let's set the properties
            LargePrime_p = largePrime_p;
            SmallPrime_q = smallPrime_q;
            Generator_g = generator_g;
            CoFactor_r = coFactor_r;

        }
        
        public BigInteger LargePrime_p {
            get; private set;
        }        
        public BigInteger SmallPrime_q
        {
            get; private set;
        }        
        public BigInteger CoFactor_r
        {
            get; private set;
        }        
        public BigInteger Generator_g
        {
            get; private set;
        }

        #region ExpectedParameters

        private readonly string largePrimeString = @"FFFFFFFF FFFFFFFF FFFFFFFF FFFFFFFF FFFFFFFF FFFFFFFF FFFFFFFF FFFFFFFF
93C467E3 7DB0C7A4 D1BE3F81 0152CB56 A1CECC3A F65CC019 0C03DF34 709AFFBD
8E4B59FA 03A9F0EE D0649CCB 621057D1 1056AE91 32135A08 E43B4673 D74BAFEA
58DEB878 CC86D733 DBE7BF38 154B36CF 8A96D156 7899AAAE 0C09D4C8 B6B7B86F
D2A1EA1D E62FF864 3EC7C271 82797722 5E6AC2F0 BD61C746 961542A3 CE3BEA5D
B54FE70E 63E6D09F 8FC28658 E80567A4 7CFDE60E E741E5D8 5A7BD469 31CED822
03655949 64B83989 6FCAABCC C9B31959 C083F22A D3EE591C 32FAB2C7 448F2A05
7DB2DB49 EE52E018 2741E538 65F004CC 8E704B7C 5C40BF30 4C4D8C4F 13EDF604
7C555302 D2238D8C E11DF242 4F1B66C2 C5D238D0 744DB679 AF289048 7031F9C0
AEA1C4BB 6FE9554E E528FDF1 B05E5B25 6223B2F0 9215F371 9F9C7CCC 69DDF172
D0D62342 17FCC003 7F18B93E F5389130 B7A661E5 C26E5421 4068BBCA FEA32A67
818BD307 5AD1F5C7 E9CC3D17 37FB2817 1BAF84DB B6612B78 81C1A48E 439CD03A
92BF5222 5A2B38E6 542E9F72 2BCE15A3 81B5753E A8427633 81CCAE83 512B3051
1B32E5E8 D8036214 9AD030AA BA5F3A57 98BB22AA 7EC1B6D0 F17903F4 E234EA60
34AA8597 3F79A93F FB82A75C 47C03D43 D2F9CA02 D03199BA CEDDD453 34DBC6B5
FFFFFFFF FFFFFFFF FFFFFFFF FFFFFFFF FFFFFFFF FFFFFFFF FFFFFFFF FFFFFFFF";


        private BigInteger ExpectedLargePrime
        {
            get
            {
                return BigInteger.Parse(Regex.Replace(largePrimeString, @"\t|\n|\r|\s+", ""), System.Globalization.NumberStyles.HexNumber);
            }
        }
        /// <summary>
        /// Standard parameters for ElectionGuard begin with the largest 256-bit prime 𝑞=2256−189.
        /// </summary>
        private BigInteger ExpectedSmallPrime
        {
            get
            {
                return BigInteger.Pow(2, 256) - 189;

            }
        }
        #endregion
    }

}
