using System;
using System.IO;
using System.Linq;
using Libble.Logchecker.Core;

namespace LogcheckerCli
{
    class Program
    {
        static void Main(string[] args)
        {
            string input, output = null;

            if (args.Length == 1)
            {
                input = args[0];
            }
            else if (args.Length == 3 && args[0] == "-o")
            {
                output = args[1];
                input = args[2];
            }
            else
            {
                Console.WriteLine("Usage: LogcheckerCli [-o OUTPUT_LOG] FILE");
                return;
            }

            string log;
            try
            {
                log = File.ReadAllText(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading log: " + ex.Message);
                return;
            }

            LogcheckerWrapper logchecker = new LogcheckerWrapper(log);

            Console.WriteLine("Score: " + logchecker.Score);
            Console.WriteLine("Good:");
            if (logchecker.Good.Length == 0)
            {
                Console.WriteLine("- None");
            }
            else
            {
                Console.WriteLine(string.Join(Environment.NewLine, logchecker.Good.Select(s => "- " + s)));
            }
            Console.WriteLine("Bad:");
            if (logchecker.Bad.Length == 0)
            {
                Console.WriteLine("- None");
            }
            else
            {
                Console.WriteLine(string.Join(Environment.NewLine, logchecker.Bad.Select(s => "- " + s)));
            }

            if (output != null)
            {
                try
                {
                    File.WriteAllText(output, logchecker.Log);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error writing output: " + ex.Message);
                }
            }
        }
    }
}
