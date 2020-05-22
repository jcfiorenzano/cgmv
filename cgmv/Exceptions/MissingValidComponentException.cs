using System;
using System.Collections.Generic;
using System.Text;

namespace cgmv.Exceptions
{
    public class MissingValidComponentException: Exception
    {
        public MissingValidComponentException()
            : base(Resources.MissingValidComponentExceptionMessage)
        { }
    }
}
