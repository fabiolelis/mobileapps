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

namespace MobileAppsProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserEdit : Page
    {
        public UserEdit()
        {
            this.InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Name = this.txtName.Text;
            user.Age = Convert.ToInt32(this.txtAge.Text);
            user.Height = Convert.ToInt32(this.txtHeight.Text);
            user.Weight = Convert.ToDouble(this.txtWeight.Text);

            user.WakeUpTime = Convert.ToInt32(this.timeWakeUpTime.Time.Minutes);
            user.LunchTime = Convert.ToInt32(this.timeLunchUpTime.Time.Minutes);
            user.DinnerTime = Convert.ToInt32(this.timeDinnerUpTime.Time.Minutes);
            user.BedTime = Convert.ToInt32(this.timeBedUpTime.Time.Minutes);

            user.Ref_energy = Convert.ToInt32(this.txtEnergy.Text);
            user.Ref_fat = Convert.ToInt32(this.txtFat.Text);
            user.Ref_saturates = Convert.ToInt32(this.txtSaturates.Text);
            user.Ref_sugar = Convert.ToInt32(this.txtSugar.Text);
            user.Ref_salt = Convert.ToInt32(this.txtSalt.Text);

            UserDB udb = new UserDB(user);
            udb.save();


        }
    }
}
