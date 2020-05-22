using Microsoft.VisualStudio.Services.Governance.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace cgmv.Exceptions
{
    public class ComponentTypeNotSupportedException: Exception
    {
        public ComponentTypeNotSupportedException(ComponentType type)
            : base(string.Format(Resources.ComponentTypeNotSupportedMessage, type))
        {
        }
    }
}
