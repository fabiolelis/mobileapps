using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppsProject.Models
{
    class Eat
    {
        private int _eatID;
        private int _mealID;
        private DateTime _time;
        private Boolean _done = false;
        private Boolean _skipped = false;//*mistyped for skipped
        private string _kind;// breakfest, lunch, dinner, snack
        private int _dayID;
        private Boolean _notified = false;

        public Eat() { }

        public Eat(int dayID, string kind){
            this.DayID = dayID;
            this.Kind = kind;
        }



        public DateTime Time
        {
            get
            {
                return _time;
            }

            set
            {
                _time = value;
            }
        }

        public bool Done
        {
            get
            {
                return _done;
            }

            set
            {
                _done = value;
            }
        }

        public bool Skipped
        {
            get
            {
                return _skipped;
            }

            set
            {
                _skipped = value;
            }
        }

        public string Kind
        {
            get
            {
                return _kind;
            }

            set
            {
                _kind = value;
            }
        }

        [Column("EatID")]
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int EatID
        {
            get
            {
                return _eatID;
            }

            set
            {
                _eatID = value;
            }
        }

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

        public bool Notified
        {
            get
            {
                return _notified;
            }

            set
            {
                _notified = value;
            }
        }

        public int MealID
        {
            get
            {
                return _mealID;
            }

            set
            {
                _mealID = value;
            }
        }
    }
}
