
using EVARest.Models.DAL;
using EVARest.Models.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonToDb {
    public class Program {
#if DEBUG
        public static void Main(string[] args) {
            if (args.Length != 1)
                throw new ArgumentException("Requires a json file to be passed.");
            var filename = args.First();
            if (!File.Exists(filename))
                throw new FileNotFoundException($"File {filename} was not found.");

            
          


        }
#endif
#if !DEBUG
        public static void Main(string[] args) {
            Console.WriteLine("This program only runs in DEBUG mode.");
        }
#endif
    }
}
