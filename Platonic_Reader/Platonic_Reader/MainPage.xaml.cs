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
        Apology book;

        public MainPage()
        {
                book = new Apology();
            InitializeComponent();
     
            masterPage.listView.ItemSelected += OnItemSelected;

            void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
            {
                var item = e.SelectedItem as MasterPageItem;
                if (item != null)
                {
                    book.bookNumber = item.Book;
                    Detail = new NavigationPage(book);
                    
                    //Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                    masterPage.listView.SelectedItem = null;
                    IsPresented = false;
                }
            }

        }
    }
}
