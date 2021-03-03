using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionGuardVerifier.Verifiers
{

    /// <summary>
    /// Holds the method that performs the verification step
    /// </summary>
    public delegate List<VerificationResultMessage> VerificationMethod();

    /// <summary>
    /// Describes the verification step and defines the results messaging
    /// </summary>
    public class VerificationStep {     
        
        /// <summary>
        /// Describes the verification step
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Sets an order for the step to run
        /// </summary>
        public int StepOrder { get; set; }

        /// <summary>
        /// Free form field to describe a successful verification result
        /// </summary>
        public string SuccessMessage { get; set; }
        
        /// <summary>
        /// Free form field to describe a failure verification result
        /// </summary>
        public string ErrorMessage { get; set; }

        public VerificationMethod VerificationStepMethod { get; set; }

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

        /// <summary>
        /// Identifies the specific object being verified
        /// </summary>
        public string Reference { get; set; }
       
    }
    /// <summary>
    /// Used to pass result back to calling verification step
    /// </summary>
    public class VerificationResultMessage
    {
        /// <summary>
        /// Identifies whether the step was successful
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Identifies the specific object being verified
        /// </summary>
        public string Reference { get; set; }
    }
}
