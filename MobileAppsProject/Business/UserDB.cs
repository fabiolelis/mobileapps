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
                //conn.CreateTable<UserTest>();
                var info = conn.GetMapping(typeof(User));
                
                // (more property assignments here) 
                var i = conn.InsertOrReplace(this._user);
                return i;

            }
        }

        public List<User> getAll()
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
