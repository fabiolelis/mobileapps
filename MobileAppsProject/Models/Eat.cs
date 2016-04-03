using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppsProject.Models
{
    class Eat
    {
        private Meal _meal;
        private DateTime _time;
        private Boolean _done = false;
        private Boolean _skippes = false;

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

        public bool Skippes
        {
            get
            {
                return _skippes;
            }

            set
            {
                _skippes = value;
            }
        }
    }
}
