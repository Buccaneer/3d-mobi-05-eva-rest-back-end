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
            const string recipesFilename = @"C:\Users\Jasper De Vrient\Documents\Tin3\Project III\ApiBackEnd\EVARest\EVARest\recipes.json";
            const string restaurantsFilename = @"C:\Users\Jasper De Vrient\Documents\Tin3\Project III\ApiBackEnd\EVARest\EVARest\restaurants.json";


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
            using (StreamReader file = File.OpenText(recipesFilename))
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

            const string res_name = "name";
            const string res_long = "lng";
            const string res_lat = "lat";
            const string res_website = "website";
            const string res_street = "street";
            const string res_postal = "postal";
            const string res_city = "city";
            const string res_phone = "phone";
            const string res_mail = "mail";
            const string res_description = "body";

            IList<Restaurant> frestaurants = new List<Restaurant>();
            using (StreamReader file = File.OpenText(restaurantsFilename))
            using (JsonTextReader reader = new JsonTextReader(file)) {
                JArray restaurants = (JArray)JToken.ReadFrom(reader);

                foreach (var restaurantJson in restaurants) {
                    Restaurant r = new Restaurant();
                    r.City = (restaurantJson[res_city] ?? string.Empty).ToString();
                    r.Name = (restaurantJson[res_name] ?? string.Empty).ToString();
                    r.Description = (restaurantJson[res_description] ?? string.Empty).ToString();
                    r.Email = (restaurantJson[res_mail] ?? string.Empty).ToString();
                    
                    r.Phone = (restaurantJson[res_phone] ?? string.Empty).ToString();

                    int value;

                    if (int.TryParse((restaurantJson[res_postal] ?? string.Empty).ToString(), out value))
                        r.Postal = value;

                    double val;
                    if (double.TryParse((restaurantJson[res_lat] ?? string.Empty).ToString().Replace(".", ","), out val))
                        r.Latitude = val;
                    else
                        r.Latitude = -1;
                    if (double.TryParse((restaurantJson[res_long] ?? string.Empty).ToString().Replace(".",","), out val))
                        r.Longitute = val;
                    else r.Longitute = -1;
                  
                    r.Street = (restaurantJson[res_street] ?? string.Empty).ToString();
                    r.Website = (restaurantJson[res_website] ?? string.Empty).ToString();
                    frestaurants.Add(r);
                }
            }
            try {
                Console.WriteLine("Attempting connection with database.");
            

                

                Console.WriteLine("Connection succeeded.");
                Console.WriteLine("Start Writing.");

                foreach (var recipe in recipeObjects) {
                    _context.Recipes.Add(recipe);
                }

                _context.SaveChanges();
                _context.Restaurants.AddRange(frestaurants);

                _context.SaveChanges();
                Console.WriteLine("Connection closed.");
            } catch (Exception ex) {
                Console.Error.WriteLine(ex.Message);
                throw   ex;
           
            }
            return Ok();
        }

        public FillerController(RestContext context) {
            _context = context;
        }
    }
}