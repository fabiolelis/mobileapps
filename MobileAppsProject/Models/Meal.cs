using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using SQLite.Net.Attributes;

namespace MobileAppsProject.Models
{
    [Table("Meal")]
    class Meal
    {
        private int _mealID;
        private int _energy;
        private int _fat;
        private int _saturates;
        private int _sugar;
        private int _salt;
        private enum _kind {BREAKFEST, LUNCH, DINNER, SNACK};
        private Image picture;

        [Column("MealID")]
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
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

        public int Sugar
        {
            get
            {
                return _sugar;
            }

            set
            {
                _sugar = value;
            }
        }

      
        public int Salt
        {
            get
            {
                return _salt;
            }

            set
            {
                _salt = value;
            }
        }

        public Image Picture
        {
            get
            {
                return picture;
            }

            set
            {
                picture = value;
            }
        }

        
    }
}
