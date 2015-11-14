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

namespace EVARest.Controllers {
    [System.Web.Http.RoutePrefix("api/Filler")]
    public class FillerController : ApiController {
        private string filename;
        private RestContext _context;
        // GET: Filler
        public IHttpActionResult Index() {
            string filename = @"C:\Users\Jasper De Vrient\Documents\Tin3\Project III\ApiBackEnd\EVARest\EVARest\recepten.json";

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
                            // if (int.TryParse(component[c_count].ToString(), out amount))
                            c.Quantity = component[c_count].ToString();

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


            }

            try {

                filename = @"C:\Users\Jasper De Vrient\Documents\Tin3\Project III\ApiBackEnd\EVARest\EVARest\restaurants.json";
                const string rest_name = "name";
                const string rest_lng = "lng";
                const string rest_lat = "lat";
                const string rest_website = "website";
                const string rest_street = "street";
                const string rest_postal = "postal";
                const string rest_city = "city";
                const string rest_phone = "phone";
                const string rest_mail = "mail";
                const string rest_body = "body";

                using (StreamReader file = File.OpenText(filename))
                using (JsonTextReader reader = new JsonTextReader(file)) {
                    JArray restaurants = (JArray)JToken.ReadFrom(reader);

                    foreach (var r in restaurants) {
                        var restaurant = new Restaurant();

                        restaurant.Name = r[rest_name].ToString();
                        if (r[rest_lng] != null) {
                            double outing;
                            if (double.TryParse(r[rest_lng].ToString(), out outing))
                                restaurant.Longitute = outing;
                            
                        }

                        if (r[rest_lat] != null) {
                            double outing;
                            if (double.TryParse(r[rest_lat].ToString(), out outing))
                                restaurant.Latitude = outing;

                        }

                        if (r[rest_postal] != null) {
                            int outing;
                            if (int.TryParse(r[rest_postal].ToString(), out outing))
                                restaurant.Postal = outing;

                        }

                        if (r[rest_body] != null)
                            restaurant.Description = r[rest_body].ToString();

                        if (r[rest_city] != null)
                            restaurant.City = r[rest_city].ToString();
                        if (r[rest_mail] != null)
                            restaurant.Email = r[rest_mail].ToString();
                        if (r[rest_phone] != null)
                            restaurant.Phone = r[rest_phone].ToString();
                        if (r[rest_street] != null)
                            restaurant.Street = r[rest_street].ToString();
                        if (r[rest_website] != null)
                            restaurant.Website = r[rest_website].ToString();
                       
                        _context.Restaurants.Add(restaurant);
                    }
                }
                _context.SaveChanges();
            } catch (Exception ex) {
                Console.Error.WriteLine(ex.Message);
            }
            return Ok();
        }

        public FillerController(RestContext context) {
            _context = context;
        }
    }
}