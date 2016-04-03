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
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MobileAppsProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HaveMeal : Page
    {
        Day _day;

        public HaveMeal()
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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter as Day;
            _day = parameter;

            cbMeal.ItemsSource = MealDB.getAll();
            cbMeal.DisplayMemberPath = "Name";
            cbMeal.SelectedValuePath = "MealID";

            cbMeal.SelectedIndex = 0;
            

        }

        private void btnHave_Click(object sender, RoutedEventArgs e)
        {
            if (_day.Eats == null)
                _day.Eats = new List<Eat> { };
            Eat eat = new Eat();
            eat.Meal = (Meal)cbMeal.SelectedItem;
            eat.Time = DateTime.Now;
            eat.Done = true;
            eat.Skippes = false;

            _day.Eats.Add(eat);
            _day.Energy += eat.Meal.Energy;
            _day.Fat += eat.Meal.Fat;
            _day.Saturates += eat.Meal.Saturates;
            _day.Sugars += eat.Meal.Sugar;
            _day.Salt += eat.Meal.Salt;

            DayDB ddb = new DayDB(_day);
            ddb.save();

            Frame.Navigate(typeof(MainPage));
        }

        private void cbMeal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Meal meal = (Meal)cbMeal.SelectedItem;
            tbName.Text = meal.Name;
            tbEnergy.Text = ("Energy " + meal.Energy.ToString());
            tbFat.Text = ("Fat " + meal.Fat.ToString());
            tbSaturates.Text = ("Saturates " + meal.Saturates.ToString());
            tbSugar.Text = ("Sugar " + meal.Sugar.ToString());
            tbSalt.Text = ("Salt " + meal.Salt.ToString());
            imgMeal.Source = new BitmapImage(new Uri("ms-appx:///"+meal.PicPath, UriKind.RelativeOrAbsolute));
            
        }
    }
}


