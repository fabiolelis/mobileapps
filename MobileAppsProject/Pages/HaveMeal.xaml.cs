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
        string _kind = "";
        List<Eat> eats = new List<Eat> { };

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
            var parameter = e.Parameter as HaveMealClass;
            _day = parameter.Day;
            _kind = parameter.Kind;
            //get eats

            cbMeal.ItemsSource = MealDB.getAll();
            cbMeal.DisplayMemberPath = "Name";
            cbMeal.SelectedValuePath = "MealID";

            cbMeal.SelectedIndex = 0;
            

        }

        private void btnHave_Click(object sender, RoutedEventArgs e)
        {

            //get eat from day by kind
            /*
            _day.Eats.Where(p => p.Kind == _kind).First().Meal = (Meal)cbMeal.SelectedItem;
            _day.Eats.Where(p => p.Kind == _kind).First().Time = DateTime.Now;
            _day.Eats.Where(p => p.Kind == _kind).First().Done = true;

            _day.Energy += _day.Eats.Where(p => p.Kind == _kind).First().Meal.Energy;
            _day.Fat += _day.Eats.Where(p => p.Kind == _kind).First().Meal.Fat;
            _day.Saturates += _day.Eats.Where(p => p.Kind == _kind).First().Meal.Saturates;
            _day.Sugars += _day.Eats.Where(p => p.Kind == _kind).First().Meal.Sugar;
            _day.Salt += _day.Eats.Where(p => p.Kind == _kind).First().Meal.Salt;

            DayDB ddb = new DayDB(_day);
            ddb.save();
            */
            
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


