using MobileAppsProject.Business;
using MobileAppsProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MobileAppsProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MealList : Page
    {
        public MealList()
        {
            this.InitializeComponent();
            mealsGridView.ItemsSource = MealDB.getAll();
           // mealsGridView.Header = new List<ListViewBaseHeaderItem> { new ListViewHeaderItem("Test") };
                //new List<string> { "Meal", "Energy", "Fat", "Kind" };


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

        private void mealsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Meal meal = e.ClickedItem as Meal;
           
            if (meal != null)
            {
                this.Frame.Navigate(typeof(MealEdit), meal);
                //TODO : Do whatever you want
            }
        }
    }
}
