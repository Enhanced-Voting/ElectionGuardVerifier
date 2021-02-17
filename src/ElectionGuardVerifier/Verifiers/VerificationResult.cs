using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionGuardVerifier.Verifiers
{
    /// <summary>
    /// Describes the verification step and defines the results messaging
    /// </summary>
    public class VerificationStep {     
        
        /// <summary>
        /// Describes the verification step
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Free form field to describe a successful verification result
        /// </summary>
        public string SuccessMessage { get; set; }
        
        /// <summary>
        /// Free form field to describe a failure verification result
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Holds individual verification step results
    /// </summary>
    public class VerificationResult
    {
        /// <summary>
        /// Identifies the verification step
        /// </summary>
        public VerificationStep VerificationStep { get; set; }

        /// <summary>
        /// Identifies whether the step was successful
        /// </summary>
        public bool IsSuccessful { get; set; }

       
    }
}
