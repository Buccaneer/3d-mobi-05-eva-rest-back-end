
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
            const string recipesFilename = @"C:\Users\Jasper De Vrient\Documents\Tin3\Project III\ApiBackEnd\EVARest\EVARest\recipes.json";
            const string restaurantsFilename = @"C:\Users\Jasper De Vrient\Documents\Tin3\Project III\ApiBackEnd\EVARest\EVARest\restaurants.json";
            //if (args.Length != 1)
            //    throw new ArgumentException("Requires a json file to be passed.");
            //var filename = args.First();
            //if (!File.Exists(filename))
            //    throw new FileNotFoundException($"File {filename} was not found.");

            //using (StreamReader file = File.OpenText(restaurantsFilename))
            //using (JsonTextReader reader = new JsonTextReader(file)) {
            //    JArray restaurants = (JArray)JToken.ReadFrom(reader);

            //    foreach (var restaurantJson in restaurants) {
            //        Restaurant r = new Restaurant();
            //        r.City = restaurantJson[res_city].tos
            //    }
            //}



        }
#endif
#if !DEBUG
        public static void Main(string[] args) {
            Console.WriteLine("This program only runs in DEBUG mode.");
        }
#endif
    }
}
