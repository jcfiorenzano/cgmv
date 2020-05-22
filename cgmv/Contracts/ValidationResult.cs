using System.Collections.Generic;

namespace cgmv.Contracts
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }

        public IList<string> Messages { get; set; } = new List<string>();
    }
}
