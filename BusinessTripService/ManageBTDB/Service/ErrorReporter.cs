using System;
using System.Collections.Generic;
using System.Text;

namespace ManageBTDB.Service
{
    class ErrorReporter : Exception
    {
        public ErrorReporter() : base() { }
        public ErrorReporter(string message) : base(message) { }
        public override string ToString()
        {
            return Message;
        }
    }
}
