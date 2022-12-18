using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateValidation
{
    internal static class Log
    {
        private static StreamWriter _writer;

        public static void Init()
        {
            _writer = new StreamWriter(File.Create("validator.log"));
            _writer.AutoFlush = true;
        }

        public static void Dispose()
        { 
            _writer.Dispose();
        }

        public static void LogInfo(String message)
        {
            _writer.WriteLine(message);
            Console.WriteLine(message);
        }

        public static void LogError(String message)
        {
            var actualmsg = "[ERROR]: " + message;
            _writer.WriteLine(actualmsg);
            Console.WriteLine(actualmsg);
        }
    }
}
