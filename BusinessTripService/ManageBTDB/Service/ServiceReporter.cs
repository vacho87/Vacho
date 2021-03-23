using System;
using System.Collections.Generic;
using System.Text;

namespace ManageBTDB.Service
{
    class ServiceReporter
    {
        public static void ShowServiceMessage(string message)
        {
            Console.WriteLine($"\n ******** {message}");
        }
    }
}
