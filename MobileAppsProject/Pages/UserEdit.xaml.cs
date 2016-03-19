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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MobileAppsProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserEdit : Page
    {
        User user = null;
        public UserEdit()
        {
            this.InitializeComponent();
           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter as User;
            // a simple controller that adds a string to a List<string>
            user = parameter;

            if (user != null)
            {
               
                this.txtName.Text = user.Name;
                this.txtAge.Text = user.Age.ToString();
                this.txtHeight.Text = user.Height.ToString();
                this.txtWeight.Text = user.Weight.ToString();

                this.timeWakeUpTime.Time = getTimeFromTotal(user.WakeUpTime);
                this.timeBreakfestTime.Time = getTimeFromTotal(user.BreakfestTime);
                this.timeLunchTime.Time = getTimeFromTotal(user.LunchTime);
                this.timeDinnerTime.Time = getTimeFromTotal(user.DinnerTime);
                this.timeBedTime.Time = getTimeFromTotal(user.BedTime);

                this.txtEnergy.Text = user.Ref_energy.ToString();
                this.txtFat.Text = user.Ref_fat.ToString();
                this.txtSaturates.Text = user.Ref_saturates.ToString();
                this.txtSugar.Text = user.Ref_sugar.ToString();
                this.txtSalt.Text = user.Ref_salt.ToString();

            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(user == null)
            {
                user = new User();
            }

            user.Name = this.txtName.Text;
            user.Age = Convert.ToInt32(this.txtAge.Text);
            user.Height = Convert.ToInt32(this.txtHeight.Text);
            user.Weight = Convert.ToDouble(this.txtWeight.Text);

            user.WakeUpTime = Convert.ToInt32(this.timeWakeUpTime.Time.TotalMinutes);
            user.BreakfestTime = Convert.ToInt32(this.timeBreakfestTime.Time.TotalMinutes);
            user.LunchTime = Convert.ToInt32(this.timeLunchTime.Time.TotalMinutes);
            user.DinnerTime = Convert.ToInt32(this.timeDinnerTime.Time.TotalMinutes);
            user.BedTime = Convert.ToInt32(this.timeBedTime.Time.TotalMinutes);

            user.Ref_energy = Convert.ToInt32(this.txtEnergy.Text);
            user.Ref_fat = Convert.ToInt32(this.txtFat.Text);
            user.Ref_saturates = Convert.ToInt32(this.txtSaturates.Text);
            user.Ref_sugar = Convert.ToInt32(this.txtSugar.Text);
            user.Ref_salt = Convert.ToInt32(this.txtSalt.Text);

            UserDB udb = new UserDB(user);
            udb.save();

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["userID"] = user.UserID;

            Frame.Navigate(typeof(MainPage));


        }

        public TimeSpan getTimeFromTotal(int total)
        {
            return new TimeSpan(total/60, total%60, 0);
            
        }
    }
}
