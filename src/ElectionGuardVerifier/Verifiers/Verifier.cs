using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionGuardVerifier.Verifiers
{
    public abstract class Verifier
    {
        protected Calculator Calculator { get; set; }

        public Verifier() {
            Calculator = new Calculator();
        }

    }
}
