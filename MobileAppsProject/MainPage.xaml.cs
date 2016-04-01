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
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MobileAppsProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        User user = null;

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

            if(user == null)
            {
                Frame.Navigate(typeof(UserEdit));

            }

            
             if (user != null)
             {
                 helloUser.Text = "Hello " + user.Name;
               //  this.UserEditBtn.Content = "Edit user";
             }
             /*
             else
             {
                 helloUser.Text = "No user";
                 this.UserEditBtn.Content = "Add user";
             }
             */
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

        private void imgSettings_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void haveMeal_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
