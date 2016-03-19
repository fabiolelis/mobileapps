using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppsProject.Models
{
    [Table("User")]

    class User
    {
        private int _userID;

        private string _name;
        private int _age;
        private int _height; //cm
        private double _weight;
    
        private int _wakeUpTime;
        private int _breakfestTime;
        private int _lunchTime;
        private int _dinnerTime;
        private int _bedTime;

        private int _ref_energy = 2000;
        private int _ref_fat = 70;
        private int _ref_saturates = 20;
        private int _ref_sugar = 90;
        private int _ref_salt = 6;

        [Column("UserID")]
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public int Age
        {
            get
            {
                return _age;
            }

            set
            {
                _age = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }
        }

        public double Weight
        {
            get
            {
                return _weight;
            }

            set
            {
                _weight = value;
            }
        }

        public int WakeUpTime
        {
            get
            {
                return _wakeUpTime;
            }

            set
            {
                _wakeUpTime = value;
            }
        }

        public int BreakfestTime
        {
            get
            {
                return _breakfestTime;
            }

            set
            {
                _breakfestTime = value;
            }
        }

        public int LunchTime
        {
            get
            {
                return _lunchTime;
            }

            set
            {
                _lunchTime = value;
            }
        }

        public int DinnerTime
        {
            get
            {
                return _dinnerTime;
            }

            set
            {
                _dinnerTime = value;
            }
        }

        public int BedTime
        {
            get
            {
                return _bedTime;
            }

            set
            {
                _bedTime = value;
            }
        }

        public int Ref_energy
        {
            get
            {
                return _ref_energy;
            }

            set
            {
                _ref_energy = value;
            }
        }

        public int Ref_fat
        {
            get
            {
                return _ref_fat;
            }

            set
            {
                _ref_fat = value;
            }
        }

        public int Ref_saturates
        {
            get
            {
                return _ref_saturates;
            }

            set
            {
                _ref_saturates = value;
            }
        }

        public int Ref_sugar
        {
            get
            {
                return _ref_sugar;
            }

            set
            {
                _ref_sugar = value;
            }
        }

        public int Ref_salt
        {
            get
            {
                return _ref_salt;
            }

            set
            {
                _ref_salt = value;
            }
        }
    }
}
