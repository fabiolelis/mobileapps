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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MobileAppsProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MealEdit : Page
    {
        Meal meal = null;
        public MealEdit()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter as Meal;
            // a simple controller that adds a string to a List<string>
            meal = parameter;

            if (meal != null)
            {

               

            }
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (meal == null)
            {
                meal = new Meal();
            }
            else
            {
                meal.Energy = Convert.ToInt32(this.txtEnergy.Text);
                meal.Fat = Convert.ToInt32(this.txtFat.Text);
                meal.Saturates = Convert.ToInt32(this.txtSaturates.Text);
                meal.Sugar = Convert.ToInt32(this.txtSugar.Text);
                meal.Salt = Convert.ToInt32(this.txtSalt.Text);

                

            }


            Frame.Navigate(typeof(MainPage));


        }
    }
}
