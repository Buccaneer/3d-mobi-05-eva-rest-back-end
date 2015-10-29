using EVARest.Models.DAL;
using EVARest.Models.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EVARest.Controllers
{
    [System.Web.Http.RoutePrefix("initDB")]
    public class FillerController : ApiController
    {
        private string filename;
        private RestContext _context;
        // GET: Filler
        public IHttpActionResult Index()
        {
            string filename = @"D:\Projecten 3\EVAVZW Rest service\EVARest\EVARest\proefdata.json";

            IDictionary<string, RecipeProperty> properties = new Dictionary<string, RecipeProperty>();
            IDictionary<string, Ingredient> ingredients = new Dictionary<string, Ingredient>();
            IList<Recipe> recipeObjects = new List<Recipe>();

            const string r_prop = "properties";
            const string r_title = "title";
            const string r_image = "image";
            const string r_description = "description";
            const string r_ingredients = "ingredients";
            const string c_count = "count";
            const string c_ingredient = "ingredient";
            const string i_name = "name";
            const string i_unit = "unit";


            int successes = 0;
            int failures = 0;
            using (StreamReader file = File.OpenText(filename))
            using (JsonTextReader reader = new JsonTextReader(file)) {
                JArray recepten = (JArray)JToken.ReadFrom(reader);

                Console.WriteLine($"{recepten.Count} Recepten gelezen uit json.");

                foreach (JObject recipeJson in recepten) {
                    try {
                        Recipe r = new Recipe();
                        r.Name = recipeJson[r_title].ToString();
                        r.Image = recipeJson[r_image].ToString();
                        r.Description = recipeJson[r_description].ToString();
                        foreach (JObject component in recipeJson[r_ingredients]) {
                            Component c = new Component();
                            int amount = 0;
                            if (int.TryParse(component[c_count].ToString(), out amount))
                                c.Quantity = amount;

                            var iname = component[c_ingredient][i_name].ToString();
                            if (!ingredients.ContainsKey(iname)) {
                                Ingredient i = new Ingredient();
                                i.Name = component[c_ingredient][i_name].ToString();
                                i.Unit = component[c_ingredient][i_unit].ToString();
                                ingredients[i.Name] = i;

                            }
                            c.Ingredient = ingredients[iname];
                            r.Ingredients.Add(c);
                        }

                        foreach (var kv in JsonConvert.DeserializeObject<Dictionary<string, string>>(recipeJson[r_prop].ToString())) {
                            var joinedkey = kv.Key + kv.Value;
                            if (!properties.ContainsKey(joinedkey))
                                properties[joinedkey] = new RecipeProperty { Type = kv.Key, Value = kv.Value };
                            r.Properties.Add(properties[joinedkey]);
                        }
                        recipeObjects.Add(r);
                        successes++;
                    } catch {
                        failures++;
                    }
                }
                Console.WriteLine($"ERROR: {failures}");
                Console.WriteLine($"SUCCES: {successes}");
            }

            try {
                Console.WriteLine("Attempting connection with database.");
            

                

                Console.WriteLine("Connection succeeded.");
                Console.WriteLine("Start Writing.");

                foreach (var recipe in recipeObjects) {
                    _context.Recipes.Add(recipe);
                }

                _context.SaveChanges();
                Console.WriteLine("Connection closed.");
            } catch (Exception ex) {
                Console.Error.WriteLine(ex.Message);
                return InternalServerError();
           
            }
            return Ok();
        }

        public FillerController(RestContext context) {
            _context = context;
        }
    }
}