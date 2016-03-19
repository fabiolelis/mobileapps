using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAppsProject.Models;
using System.IO;


namespace MobileAppsProject.Business
{
    class MealDB
    {
        private Meal _meal;

        internal Meal Meal
        {
            get
            {
                return _meal;
            }

            set
            {
                _meal = value;
            }
        }

        public MealDB(Meal meal)
        {
            this._meal = meal;
        }

        public int save()
        {
            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                var infoTable = conn.GetTableInfo("Meal");

                if (!infoTable.Any())
                {
                    conn.CreateTable<User>();

                }
                var info = conn.GetMapping(typeof(User));

                if (this._meal.MealID == 0)
                {
                    var i = conn.Insert(this._meal);
                    conn.Commit();
                    return i;
                }
                else
                {
                    var i = conn.Update(this._meal);
                    return i;
                }


            }
        }
    }
}
