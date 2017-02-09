using System;
using System.IO;

namespace Executer
{
    class Program
    {
        private static string path = @"C:\Projects\Web\orolia\SampleFiles";
        static void Main(string[] args)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                Console.WriteLine(file);
                //foreach (var data in ParceFile(file))
                //{
                //    Console.WriteLine(data.Mark + " " + data.Value);
                //}
                

            }
        }

 
    }
}
