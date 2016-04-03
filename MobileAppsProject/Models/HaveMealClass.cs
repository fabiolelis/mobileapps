using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppsProject.Models
{
    class HaveMealClass
    {
        Day _day;
        string _kind;

        public HaveMealClass(Day day, string kind)
        {
            _day = day;
            _kind = kind;
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

        internal Day Day
        {
            get
            {
                return _day;
            }

            set
            {
                _day = value;
            }
        }
    }
}
