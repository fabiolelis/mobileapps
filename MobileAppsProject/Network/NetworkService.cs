using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAppsProject.Models;
using MobileAppsProject.Business;
using Windows.Web.Http;
using Windows.Data.Json;

namespace MobileAppsProject.Network
{
    class NetworkService
    {
        public static async void updateMeals()
        {
            /*
            private int _mealID;
            private string _name;
            private int _energy;
            private int _fat;
            private int _saturates;
            private int _sugar;
            private int _salt;
            private string _picPath;
            private Boolean _userSet = true;
            0 = Breakfest, 1 = LUNCH, 2 = DINNER, 3 = SNACK 
            private int _kind;

           */
            /*
            example
            {
              "Meals":[
                {
                    "name" : "Toast (json1)",
                    "energy" : 501,
                    "fat" : 10,
                    "saturates" : 3,
                    "sugar" : 2,
                    "salt" : 3,
                    "pic_path" :  "Assets/meals/example.jpeg",
                    "user_set" : false,
                    "kind" : 0
                },
                {
                    "name" : "Pasta (json 2)",
                    "energy" : 501,
                    "fat" : 10,
                    "saturates" : 3,
                    "sugar" : 2,
                    "salt" : 3,
                    "pic_path" :  "Assets/meals/example.jpeg",
                    "user_set" : false,
                    "kind" : 0
                }
              ]
            }

            */

            string stringUri = "http://lelis2008.cloudapp.net/mobileapps/newmeals.json";
            //(future link on azure)
           // stringUri = "http://lelis2008.cloudapp.net/greencampus/www/service.php";
            //stringUri = "http://food2fork.com/api/search?key=323650dff17c671b1896e503d35ebcf2&q=shredded%20chicken";

            var httpClient = new HttpClient();
          //  httpClient.DefaultRequestHeaders.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // var uri = new Uri("ms-appx:///" + stringUri, UriKind.RelativeOrAbsolute);
            var uri = new Uri(stringUri);
            
            try
            {


                HttpResponseMessage result = await httpClient.GetAsync(uri);

                JsonObject jsonObject = JsonObject.Parse(result.Content.ToString());

               
                foreach (IJsonValue jsonValues in jsonObject.GetNamedArray("Meals", new JsonArray()))
                {
                    try
                    {
                        JsonObject jsonValue = JsonObject.Parse(jsonValues.ToString());
                        Meal meal = new Meal();
                        meal.Name = jsonValue.GetNamedString("name", "");
                        meal.Energy = (int)jsonValue.GetNamedNumber("energy", 0);
                        meal.Fat = (int)jsonValue.GetNamedNumber("fat", 0);
                        meal.Saturates = (int)jsonValue.GetNamedNumber("saturates", 0);
                        meal.Sugar = (int)jsonValue.GetNamedNumber("sugar", 0);
                        meal.Salt = (int)jsonValue.GetNamedNumber("salt", 0);
                        meal.PicPath = jsonValue.GetNamedString("pic_path", "");
                        meal.UserSet = jsonValue.GetNamedBoolean("user_set", false);
                        meal.Kind = (int)jsonObject.GetNamedNumber("kind", 3);

                        new MealDB(meal).save();
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }


           


               
        }
    }
}
