using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Runner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var arguments = ReadArguments(args);

            var logFiles = Directory.EnumerateFiles(arguments.Input.FullName, "*.log", SearchOption.AllDirectories);

            var outputFileName = Path.Combine(arguments.Output.FullName, $"{Guid.NewGuid()}.log");

            using (var output = new StreamWriter(outputFileName, false, new UTF8Encoding(false)))
            {
                output.WriteLine("#Software: Microsoft Internet Information Services 8.0");
                output.WriteLine(
                    "#Fields: date time s-sitename cs-method cs-uri-stem cs-uri-query s-port cs-username c-ip cs(User-Agent) cs(Cookie) cs(Referer) cs-host sc-status sc-substatus sc-win32-status sc-bytes cs-bytes time-taken");

                foreach (var logFile in logFiles)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Reading '{logFile}'");
                    Console.WriteLine();

                    using (var input = new StreamReader(logFile, Encoding.UTF8))
                    {
                        var lineNumber = 0;
                        string line;

                        while ((line = await input.ReadLineAsync()) != null)
                        {
                            lineNumber++;

                            if (lineNumber < 3) continue;

                            await output.WriteLineAsync(line);
                        }

                        Console.WriteLine($"\tRead {lineNumber} line(s)");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Done - output written to '{outputFileName}'");
        }

        private static Arguments ReadArguments(string[] args)
        {
            if (args.Length > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(args),
                    "We expect two arguments: first the input directory and then the output directory.");
            }

            var inputDirectory = args[0];

            if (!Directory.Exists(inputDirectory))
            {
                throw new ArgumentOutOfRangeException(nameof(args), inputDirectory,
                    "The provided input directory does not exist");
            }

            var outputDirectory = args[1];

            if (!Directory.Exists(outputDirectory))
            {
                throw new ArgumentOutOfRangeException(nameof(args), outputDirectory,
                    "The provided outputDirectory directory does not exist");
            }

            var input = new DirectoryInfo(inputDirectory);
            var output = new DirectoryInfo(outputDirectory);

            if (input.FullName.Equals(output.FullName))
            {
                throw new ArgumentOutOfRangeException(nameof(args), "Input and ouput directories cannot be identical.");
            }

            return new Arguments(input, output);
        }
    }
}