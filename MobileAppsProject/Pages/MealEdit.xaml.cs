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
using Windows.UI.Core;
using MobileAppsProject.Business;
using Windows.Media.Capture;
using Windows.Storage;

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

            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

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
            var parameter = e.Parameter as Meal;
            // a simple controller that adds a string to a List<string>
            meal = parameter;

            if (meal != null)
            {
                this.txtName.Text = meal.Name;
                this.txtEnergy.Text = meal.Energy.ToString();
                this.txtFat.Text = meal.Fat.ToString();
                this.txtSaturates.Text = meal.Saturates.ToString();
                this.txtSugar.Text = meal.Sugar.ToString();
                this.txtSalt.Text = meal.Salt.ToString();
                this.Kind.SelectedIndex = meal.Kind;


            }
           
        }

        private async void getImage()
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }
        }


        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            getImage();

            /*
            if (meal == null)
            {
                meal = new Meal();
            }

            meal.Name = this.txtName.Text;
            meal.Energy = Convert.ToInt32(this.txtEnergy.Text);
            meal.Fat = Convert.ToInt32(this.txtFat.Text);
            meal.Saturates = Convert.ToInt32(this.txtSaturates.Text);
            meal.Sugar = Convert.ToInt32(this.txtSugar.Text);
            meal.Salt = Convert.ToInt32(this.txtSalt.Text);
            meal.Kind = this.Kind.SelectedIndex;
            meal.PicPath = "Assets/meals/example.jpeg";




            MealDB mdb = new MealDB(meal);
            mdb.save();

            Frame.Navigate(typeof(MainPage));
            */

        }
    }
}
