using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Platonic_Reader
{
    public partial class App : Application
    {
        public string resourceName;
        public App()
        {
            //InitializeComponent();

            MainPage = new Platonic_Reader.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts         
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
