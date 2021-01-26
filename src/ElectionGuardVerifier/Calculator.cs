﻿using System;
using System.Numerics;

namespace ElectionGuardVerifier
{
     /// <summary>
     /// Written based on ElectionGuard_Specification_v095.pdf
     /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Hashes are computed using the SHA-256 hash function in NIST (2015) Secure Hash Standard (SHS) which is published in FIPS 180-4 and can be found in https://csrc.nist.gov/publications/detail/fips/180/4/final.
        /// For the purposes of SHA-256 hash computations, all inputs – whether textural or numeric – are represented as utf-8 encoded strings.Numbers are represented as strings in base ten.The hash function expects a single-dimensional array of input elements that are hashed iteratively, rather than concatenated together. Each element in the hash is separated by the pipe character (“|”). When dealing with multi-dimensional arrays, the elements are hashed recursively in the order in which they are input into the hash function.For instance, calling 𝐻(1,2, [3,4,5],6) is a function call with 4 parameters, where the 3rd parameter is itself an array.The hash function will process arguments 1 and 2, when it gets to argument 3 it will traverse the array(and hash the values 3, 4 ,5) before hashing the final fourth argument(whose value is 6). When hashing an array element that is empty, the array is instead replaced with the word “null” as a placeholder.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public BigInteger Hash(string[] s)
        {
            return new BigInteger();
        }

        /// <summary>
        /// To compute (𝑎+𝑏) mod 𝑛, one can compute ((𝑎 mod 𝑛)+(𝑏 mod 𝑛)) mod 𝑛. However, this is rarely beneficial. If it is known that 𝑎,𝑏∈ℤ𝑛, then one can choose to avoid the division normally inherent in the modular reduction and just use (𝑎+𝑏) mod 𝑛=𝑎+𝑏 (if 𝑎+𝑏<𝑛) or 𝑎+𝑏−𝑛 (if 𝑎+𝑏≥𝑛).
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public BigInteger Sum(BigInteger a, BigInteger b, BigInteger n)
        {
            return new BigInteger();
        }

        /// <summary>
        /// To compute (𝑎×𝑏) mod 𝑛, one can compute ((𝑎 mod 𝑛)×(𝑏 mod 𝑛)) mod 𝑛. Unless it is already known that 𝑎,𝑏∈ℤ𝑛, it is usually beneficial to perform modular reduction on these intermediate values before computing the product. However, it is still necessary to perform modular reduction on the result of the multiplication.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public BigInteger Multiply(BigInteger a, BigInteger b, BigInteger n)
        {
            return new BigInteger();
        }

        /// <summary>
        /// To compute 𝑎𝑏 mod 𝑛, one can compute (𝑎 mod 𝑛)𝑏 mod 𝑛, but one should not perform a modular reduction on the exponent.24 One should, however, never simply attempt to compute the exponentiation 𝑎𝑏 before performing a modular reduction as the number 𝑎𝑏 would likely contain more digits then there are particles in the universe. Instead, one should use a special-purpose modular exponentiation method such as repeated squaring which prevents intermediate values from growing excessively large. Some languages include a native modular exponentiation primitive, but when this is not available a specialized modular exponentiation tool can be imported or written from scratch.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public BigInteger Exponentiate(BigInteger a, BigInteger b, BigInteger n)
        {
            return new BigInteger();
        }
    }
}
