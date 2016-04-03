using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAppsProject.Models;
using System.IO;

namespace MobileAppsProject.Business
{
    class EatDB
    {
        private Eat _eat;

        internal Eat Eat
        {
            get
            {
                return _eat;
            }

            set
            {
                _eat = value;
            }
        }

        public EatDB(Eat eat)
        {
            this._eat = eat;
        }

        public static List<Eat> getByDayID(int dayID)
        {
            List<Eat> lm = new List<Eat> { };

            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                lm = (from m in conn.Table<Eat>()
                      where m.DayID == dayID 
                      select m
                      ).ToList();

            }

            return lm;
        }

        public static List<Eat> getAll()
        {
            List<Eat> lm = new List<Eat> { };

            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                lm = (from m in conn.Table<Eat>()
                      select m).ToList();

            }

            return lm;
        }

        public int save()
        {
            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                var infoTable = conn.GetTableInfo("Eat");

                if (!infoTable.Any())
                {
                    //conn.DropTable<Eat>();
                    conn.CreateTable<Eat>();

                }
                var info = conn.GetMapping(typeof(Eat));

                if (this._eat.EatID == 0)
                {
                    var i = conn.Insert(this._eat);
                    conn.Commit();
                    return i;
                }
                else
                {
                    var i = conn.Update(this._eat);
                    return i;
                }


            }
        }
    }
}
