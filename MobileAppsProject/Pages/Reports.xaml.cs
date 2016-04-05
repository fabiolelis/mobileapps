using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MobileAppsProject.Models;
using MobileAppsProject.Business;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MobileAppsProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Reports : Page
    {
        public Reports()
        {
            this.InitializeComponent();



            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    a.Handled = true;
                }
            };

            updateText();


        }
        public void updateText()
        {

            List<Eat> eats = new List<Eat> { };
            string text = "";

            Day d = DayDB.getDatByDate(dtPicker.Date);
            if (d == null)
            {
                text = "There is no records for this day";
                tbDays.MaxLines = 100;
                tbDays.Text = text;

                return;
            }
            eats = EatDB.getByDayID(d.DayID);
            int energy = 0, fat = 0, saturates = 0, sugar = 0, salt = 0;
            if (eats != null)
            {
                foreach (Eat e in eats)
                {
                    if (e.Done && e.Meal != null)
                    {
                        text += e.Meal.Name + "\n";
                        energy += e.Meal.Energy;
                        fat += e.Meal.Fat;
                        saturates += e.Meal.Saturates;
                        sugar += e.Meal.Sugar;
                        salt += e.Meal.Salt;
                    }

                }
            }



            text += "\n Energy: " + energy;
            text += "\n Fat: " + fat;
            text += "\n Saturates: " + saturates;
            text += "\n Sugar: " + sugar;
            text += "\n Salt: " + salt;

            tbDays.MaxLines = 100;
            tbDays.Text = text;

        }




        private void dtPicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            updateText();
        }
    }
}
