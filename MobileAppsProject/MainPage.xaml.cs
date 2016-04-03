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
using MobileAppsProject.Pages;
using Windows.Web.Http;
using System.Diagnostics;
using Windows.UI.Core;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.System.Threading;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MobileAppsProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        User user = null;
        Day _day = null;
        DispatcherTimer myStopwatchTimer;

        public MainPage()
        {
            this.InitializeComponent();
            
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values["userID"] != null)
            {
                int userID = (int)localSettings.Values["userID"];
                user = UserDB.getByUserID(userID);
            }
            if (localSettings.Values["dayID"] != null)
            {
                int dayID = (int)localSettings.Values["dayID"];
                _day = DayDB.getByDayID(dayID);
            }
            

            if (user == null)
            {
                user = new User();
                helloUser.Text = "Go to menu and edit you info ->";

            }


            if (user != null)
             {
                 helloUser.Text = "Hello, " + user.Name +"!";
               //  this.UserEditBtn.Content = "Edit user";
             }
            

            myStopwatchTimer = new DispatcherTimer();
            myStopwatchTimer.Tick += changeUI;
            myStopwatchTimer.Interval = new TimeSpan(0, 0, 0, 10, 0); // 1 second
            myStopwatchTimer.Start();
            changeUI(null, null);

        }

        private void changeUI(object sender, object e)
        {
            //Get the closest future meal


            if (this._day == null || (this._day.Date.Date.Year != DateTime.Now.Date.Year || this._day.Date.Date.DayOfYear != DateTime.Now.Date.DayOfYear))
            {
                //create day

                this._day = new Day();
                this._day.User = this.user;
                this._day.Date = DateTime.Now;
                this._day.LastMeal = (int)DateTime.Now.TimeOfDay.TotalMinutes;
                DayDB ddb = new DayDB(this._day);

                ddb.save();

                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["dayID"] = _day.DayID;

            }

            if (DateTime.Now.TimeOfDay.TotalMinutes > user.DinnerTime)
            {
                this.countdown.Text = "no more food today...";
                this.rectTime.Width = 0;
            }
            else
            {
                TimeSpan until = untilNext();

                this.countdown.Text = until.Hours.ToString() + " hours and " + until.Minutes.ToString() + " minutes";
                // fix it:
                //this.rectTime.Width = 200 * ((DateTime.Now.TimeOfDay.TotalMinutes - (double)_day.LastMeal) - (double)until.TotalMinutes));
            }
        }

        public TimeSpan untilNext()
        {

            int[] diff = new int[3];
            diff[0] = user.BreakfestTime - (int)DateTime.Now.TimeOfDay.TotalMinutes;
            diff[1] = user.LunchTime - (int)DateTime.Now.TimeOfDay.TotalMinutes;
            diff[2] = user.DinnerTime - (int)DateTime.Now.TimeOfDay.TotalMinutes;

            int min = Int32.MaxValue;
            for (int i = 0; i < 3; i++)
            {
                if (diff[i] < 0)
                    diff[i] = Int32.MaxValue;

                if (diff[i] < min)
                {
                    min = diff[i];

                }

            }
            TimeSpan ts = getTimeFromTotal(min);
            return ts;

        }


        
        private void UserEditBtn_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Values["userID"] != null)
            {
                Frame.Navigate(typeof(UserEdit), user);
            }
            else
            {
                Frame.Navigate(typeof(UserEdit));
            }
                
        }

        private void ListMealbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MealList));
        }


        private void AddMealbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MealEdit));
           /* var uri = new Uri("http://google.com");
            var httpClient = new HttpClient();

            // Always catch network exceptions for async methods
            try
            {
                var result = await httpClient.GetStringAsync(uri);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            // Once your app is done using the HttpClient object call dispose to 
            // free up system resources (the underlying socket and memory used for the object)
            httpClient.Dispose();
            */

        }

        private void haveMeal_Click(object sender, RoutedEventArgs e)
        {

        }


        public TimeSpan getTimeFromTotal(int total)
        {
            return new TimeSpan(total / 60, total % 60, 0);

        }
    }

}
