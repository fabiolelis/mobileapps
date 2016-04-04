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
using Windows.UI;
using Windows.UI.Notifications;
using MobileAppsProject.Network;
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
        string nextMeal = "";


        public MainPage()
        {
            this.InitializeComponent();
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            getStorageInfo();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

                changeUI(null, null);
                myStopwatchTimer = new DispatcherTimer();
                myStopwatchTimer.Tick += changeUI;
                myStopwatchTimer.Interval = new TimeSpan(0, 0, 0, 10, 0); // 1 second
                myStopwatchTimer.Start();

                this.tbDate.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            // _day = null;
                //NetworkService.updateMeals();

        }

        private void changeUI(object sender, object e)
        {
            if(user == null)
            {
                helloUser.Text = "Access menu button to add user";
                return;
            }
            
            updateDay();
            checkAndNotify();
            updateDayReport();

        }

        public void checkAndNotify()
        {
            //check if it is time
            string current = getCurrentMeal();

            if (!current.Equals(""))
            {

                Eat eat = null;
                eat = EatDB.getByDayID(_day.DayID).Where(et => et.Kind == current).First() as Eat;

                if (!eat.Notified)
                {


                    eat.Notified = true;
                    EatDB edb = new EatDB(eat);
                    edb.save();
                    //notify:
                    string toast = "<toast>"
                            + "<visual>"
                            + "<binding template = \"ToastGeneric\" >"
                            + "<text> Time to eat! </text>"
                            + "</binding>"
                            + "</visual>"
                            + "<audio src=\"ms - winsoundevent:Notification.Reminder\"/>"
                            + "</toast>";


                    Windows.Data.Xml.Dom.XmlDocument toastDOM = new Windows.Data.Xml.Dom.XmlDocument();
                    toastDOM.LoadXml(toast);

                    ToastNotification toastNotification = new ToastNotification(toastDOM);

                    var toastNotifier = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier();
                    toastNotifier.Show(toastNotification);

                }
                this.tbNextMeal.Text = "It is time for your";
                this.countdown.Text = current + " now!";
                rectTime.Visibility = Visibility.Visible;
                rectTimeBG.Visibility = Visibility.Visible;
                countdown.Visibility = Visibility.Visible;

            }
            else
            {
                rectTime.Visibility = Visibility.Collapsed;
                rectTimeBG.Visibility = Visibility.Collapsed;
                countdown.Visibility = Visibility.Collapsed;
                updateCountdown();
            }


        }

        public void getStorageInfo()
        {

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


            if (user != null)
                helloUser.Text = "Hello, " + user.Name + "!";

        }

        public void updateDay()
        {

           // _day = null;
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

                List<Eat> eats = new List<Eat> { };
                eats.Add(new Eat(_day.DayID, "Breakfest"));
                eats.Add(new Eat(_day.DayID, "Lunch"));
                eats.Add(new Eat(_day.DayID, "Dinner"));

                foreach(Eat e in eats)
                {
                    EatDB edb = new EatDB(e);
                    edb.save();
                }

                this.tbDate.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();

                //Update meals from servers everyday
                NetworkService.updateMeals();

            }



        }

        public void updateCountdown()
        {
            if(EatDB.getByDayID(_day.DayID).Where(d => d.Done == false).ToList().Count == 0 ||
                DateTime.Now.TimeOfDay.TotalMinutes > user.DinnerTime)
            {
                this.tbNextMeal.Text = "no more food today...";
                this.rectTime.Width = 0;
            }
            else
            {
                int nextMealTime = 0;
                string nextMeal = getNextMeal(ref nextMealTime);
                TimeSpan until = untilNext(nextMeal);

                this.tbNextMeal.Text = nextMeal + " in";
                this.countdown.Text = until.Hours.ToString() + " hours and " + until.Minutes.ToString() + " minutes";


                double ratio = (DateTime.Now.TimeOfDay.TotalMinutes - (double)_day.LastMeal) / (double)nextMealTime;
                this.rectTime.Width  = ratio < 1 ? 200 * ratio : 1;
            }
        }

        public void updateDayReport()
        {
            double ratio;
            ratio = (double)_day.Energy / (double)user.Ref_energy;
            this.rectEnergy.Width = 100 * ratio;
            this.tbEnergy.Text = "Energy " + String.Format("{0:0.00}", ratio * 100) + "%";
            if (ratio > 1)
            {
                this.rectEnergy.Width = 100;
                this.rectEnergy.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            }

            ratio = (double)_day.Fat / (double)user.Ref_fat;
            this.rectFat.Width = 100 * ratio;
            this.tbFat.Text = "Fat " + String.Format("{0:0.00}", ratio * 100) + "%";
            if (ratio > 1)
            {
                this.rectFat.Width = 100;
                this.rectFat.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            }

            ratio = (double)_day.Saturates / (double)user.Ref_saturates;
            this.rectSaturates.Width = 100 * ratio;
            this.tbSaturates.Text = "Saturates " + String.Format("{0:0.00}", ratio * 100) + "%";
            if (ratio > 1)
            {
                this.rectSaturates.Width = 100;
                this.rectSaturates.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            }

            ratio = (double)_day.Sugars / (double)user.Ref_sugar;
            this.rectSugar.Width = 100 * ratio;
            this.tbSugar.Text = "Sugar " + String.Format("{0:0.00}", ratio * 100) + "%";
            if (ratio > 1)
            {
                this.rectSugar.Width = 100;
                this.rectSugar.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            }

            ratio = (double)_day.Salt / (double)user.Ref_salt;
            this.rectSalt.Width = 100 * ratio;
            this.tbSalt.Text = "Salt " + String.Format("{0:0.00}", ratio * 100) + "%";
            if (ratio > 1)
            {
                this.rectSalt.Width = 100;
                this.rectSalt.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            }
        }

        public string getCurrentMeal()
        {
            string currentMeal = "";
            Eat eat = EatDB.getByDayID(_day.DayID).Where(et => et.Kind == "Dinner").First() as Eat;
            if (user.DinnerTime < DateTime.Now.TimeOfDay.TotalMinutes && user.DinnerTime +60  > DateTime.Now.TimeOfDay.TotalMinutes  && !eat.Done && !eat.Skipped)
            {
                currentMeal = "Dinner";
            }

            eat = EatDB.getByDayID(_day.DayID).Where(et => et.Kind == "Lunch").First() as Eat;
            if (user.LunchTime < DateTime.Now.TimeOfDay.TotalMinutes && user.LunchTime +60 > DateTime.Now.TimeOfDay.TotalMinutes && !eat.Done && !eat.Skipped)
            {
                currentMeal = "Lunch";
            }

            eat = EatDB.getByDayID(_day.DayID).Where(et => et.Kind == "Breakfest").First() as Eat;
            if (user.BreakfestTime < DateTime.Now.TimeOfDay.TotalMinutes && user.BreakfestTime +60 > DateTime.Now.TimeOfDay.TotalMinutes && !eat.Done && !eat.Skipped)
            {
                currentMeal = "Breakfest";

            }
            nextMeal = currentMeal;
            return currentMeal;

        }

        public string getNextMeal(ref int nextMealTime)
        {
            nextMeal = "";
            Eat eat = EatDB.getByDayID(_day.DayID).Where(et => et.Kind == "Dinner").First() as Eat;
            if (user.DinnerTime > DateTime.Now.TimeOfDay.TotalMinutes && !eat.Done && !eat.Skipped)
            {
                nextMeal = "Dinner";
                nextMealTime = user.DinnerTime;
            }

            eat = EatDB.getByDayID(_day.DayID).Where(et => et.Kind == "Lunch").First() as Eat;
            if (user.LunchTime > DateTime.Now.TimeOfDay.TotalMinutes && !eat.Done && !eat.Skipped)
            {
                nextMeal = "Lunch";
                nextMealTime = user.LunchTime;

            }

            eat = EatDB.getByDayID(_day.DayID).Where(et => et.Kind == "Breakfest").First() as Eat;
            if (user.BreakfestTime > DateTime.Now.TimeOfDay.TotalMinutes && !eat.Done && !eat.Skipped)
            {
                nextMeal = "Breakfest";
                nextMealTime = user.BreakfestTime;

            }

            return nextMeal;

        }

        public TimeSpan untilNext(string next)
        {
            int nextTime = 0;
            if (next.Equals("Breakfest"))
                nextTime = user.BreakfestTime;
            if (next.Equals("Lunch"))
                nextTime = user.LunchTime;
            if (next.Equals("Dinner"))
                nextTime = user.DinnerTime;

            TimeSpan ts = getTimeFromTotal(nextTime - (int)DateTime.Now.TimeOfDay.TotalMinutes);
            return ts;

        }

        public TimeSpan getTimeFromTotal(int total)
        {
            return new TimeSpan(total / 60, total % 60, 0);

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
            Frame.Navigate(typeof(HaveMeal), new HaveMealClass(_day, nextMeal));
        }

        private void haveSnack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HaveMeal), new HaveMealClass(_day, "Snack"));
        }
    }

}
