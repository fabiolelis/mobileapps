using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAppsProject.Models;
using System.IO;

namespace MobileAppsProject.Business
{
    class DayDB
    {
        private Day _Day;

        internal Day Day
        {
            get
            {
                return _Day;
            }

            set
            {
                _Day = value;
            }
        }

        public DayDB(Day Day)
        {
            this._Day = Day;
        }

        public static List<Day> getAll()
        {
            List<Day> lm = new List<Day> { };

            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                lm = (from m in conn.Table<Day>()
                      select m).ToList();

            }

            return lm;
        }

        public static Day getByDayID(int dayID)
        {
            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            Day day = new Day();

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                day = (from d in conn.Table<Day>()
                        where d.DayID == dayID
                        select d
                         ).ToList().FirstOrDefault();
            }
            return day;
        }

        public int save()
        {
            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                var infoTable = conn.GetTableInfo("Day");

                if (!infoTable.Any())
                {
                   // conn.DropTable<Day>();
                    conn.CreateTable<Day>();

                }
                var info = conn.GetMapping(typeof(Day));

                if (this._Day.DayID == 0)
                {
                    var i = conn.Insert(this._Day);
                    conn.Commit();
                    return i;
                }
                else
                {
                    var i = conn.Update(this._Day);
                    return i;
                }


            }
        }
    }
}
