using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MobileAppsProject.Models;

namespace MobileAppsProject.Business
{
    class UserDB
    {
        private User _user;

        public User User
        {
            get
            {
                return _user;
            }

            set
            {
                _user = value;
            }
        }

        public UserDB(User user)
        {
            this._user = user;
        }



        public int save()
        {
            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                var infoTable = conn.GetTableInfo("User");

                if (!infoTable.Any())
                {
                  //  conn.DropTable<User>();
                    conn.CreateTable<User>();

                }
                var info = conn.GetMapping(typeof(User));

                if (this._user.UserID == 0)
                { 
                    //var i = conn.InsertOrReplace(this._user);
                    var i = conn.Insert(this._user);
                    conn.Commit();
                   // this._user.UserID = i;
                    return i;
                }
                else
                {
                    var i = conn.Update(this._user);
                    return i;
                }
                

            }
        }

        public static User getByUserID(int userID)
        {
            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            User user = new User();

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                user = (from u in conn.Table<User>()
                         where u.UserID == userID
                         select u
                         ).ToList().FirstOrDefault();
            }
            return user;
        }

        public static List<User> getAll()
        {
            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            List<User> users = new List<User> { };

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                users = (from u in conn.Table<User>()
                         select u).ToList();
            }
            return users;

        }

    }
}
