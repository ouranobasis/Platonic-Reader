using System;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Platonic_Reader
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            masterPage.listView.ItemSelected += OnItemSelected;

            void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
            {
                var item = e.SelectedItem as MasterPageItem;
                if (item != null)
                {
                    var nav = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                    nav.BackgroundColor = Color.FromHex("#49994d");
                    nav.BarTextColor = Color.White;
                    Detail = nav;
                    masterPage.listView.SelectedItem = null;
                    IsPresented = false;
                }
            }
        }
    }
}
