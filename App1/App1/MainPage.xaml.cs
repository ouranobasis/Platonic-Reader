using System;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace App1
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            List<Apology> tabs = new List<Apology>();

            var Crito = new Apology();
            var Phaedo = new Apology();

            string concatenatedauthors = "";
            string concatenatedtitle = "";
            foreach (var item in GetLibraryCollection())
            {
                //TODo Make the tab creation dynamic using list/dictionary object to create a dynamically named page object
                //tabs.Add(new Apology());
                //tabs.
                concatenatedauthors += $"{item.Author} ";
                concatenatedtitle += $"{item.Title} ";
            }

            Crito.Title = "CRITO";
            Phaedo.Title = "PHAEDO";
            Children.Add(Crito);
            Children.Add(Phaedo);

            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            InitializeComponent();
        }

        private static List<TextItem> GetLibraryCollection()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            string[] resourceName = assembly.GetManifestResourceNames();
            List<TextItem> libraryOfTexts = new List<TextItem>();
            int increment = 0;

            foreach (var item in resourceName)
            {
                TextItem text = new TextItem();
                var stream = assembly.GetManifestResourceStream(item);

                using (XmlReader textName = XmlReader.Create(stream))
                {
                    if (textName.IsStartElement() && textName.Name == "text")
                        text.Author = (textName.GetAttribute("author") == "") ? "Anonymous" : textName.GetAttribute("author");
                    text.Title = (textName.GetAttribute("title") == "") ? "Untitled" : textName.GetAttribute("title");
                    text.Id = increment;
                    text.FileName = item;
                    increment++;
                }
                libraryOfTexts.Add(text);
            }
            return libraryOfTexts;
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
        }
    }
}
