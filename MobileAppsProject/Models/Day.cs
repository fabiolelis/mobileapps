using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppsProject.Models
{
    class Day
    {
        private int _dayID;
        private User _user;
        private List<Meal> _meals;
        private int _energy;
        private int _fat;
        private int _saturates;
        private int _sugars;
        private DateTime _date;
        private int lastMeal;

        [Column("UserID")]
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int DayID
        {
            get
            {
                return _dayID;
            }

            set
            {
                _dayID = value;
            }
        }

        internal User User
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

        internal List<Meal> Meals
        {
            get
            {
                return _meals;
            }

            set
            {
                _meals = value;
            }
        }

        public int Energy
        {
            get
            {
                return _energy;
            }

            set
            {
                _energy = value;
            }
        }

        public int Fat
        {
            get
            {
                return _fat;
            }

            set
            {
                _fat = value;
            }
        }

        public int Saturates
        {
            get
            {
                return _saturates;
            }

            set
            {
                _saturates = value;
            }
        }

        public int Sugars
        {
            get
            {
                return _sugars;
            }

            set
            {
                _sugars = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
            }
        }

        public int LastMeal
        {
            get
            {
                return lastMeal;
            }

            set
            {
                lastMeal = value;
            }
        }
    }
}
