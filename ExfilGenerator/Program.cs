using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomDataGenerator
{
    class Program
    {

        static string usage = "RandomDataGenerator.exe file:path/to/file.txt size:30";

        // Generate a random string with a given size    
        public static void RandomString(string file,Int64 sizeMb)
        {
            // Note: block size must be a factor of 1MB to avoid rounding errors :)
            const int blockSize = 1024 * 8;
            const int blocksPerMb = (1024 * 1024) / blockSize;
            byte[] data = new byte[blockSize];
            Random rng = new Random();
            using (FileStream stream = File.OpenWrite(file))
            {
                // There 
                for (int i = 0; i < sizeMb * blocksPerMb; i++)
                {
                    rng.NextBytes(data);
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        static void Main(string[] args)
        {
            var arguments = new Dictionary<string, string>();
            try
            {
                foreach (var argument in args)
                {
                    var idx = argument.IndexOf(':');
                    if (idx > 0)
                        arguments[argument.Substring(0, idx)] = argument.Substring(idx + 1);
                    else
                        arguments[argument] = string.Empty;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("[-] Exception parsing arguments:");
                Console.WriteLine(ex);
            }

            if (!arguments.ContainsKey("file"))
            {
                Console.WriteLine("[-] Error: No file  was given.");
                Console.WriteLine("Usage: " + usage);
                Environment.Exit(1);
            }
            if (!arguments.ContainsKey("size"))
            {
                Console.WriteLine("[-] Error: No size (mb) was given.");
                Console.WriteLine("Usage: " + usage);
                Environment.Exit(1);
            }


            RandomString(arguments["file"],Int64.Parse(arguments["size"]));
        }
    }
}
