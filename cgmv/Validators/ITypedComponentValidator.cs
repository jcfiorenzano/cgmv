using Microsoft.VisualStudio.Services.Governance.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace cgmv.Validators
{
    interface ITypedComponentValidator
    {
        bool IsValid(TypedComponent typedComponent);
    }
}
