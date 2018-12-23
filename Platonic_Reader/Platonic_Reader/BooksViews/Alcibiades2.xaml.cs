using System;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Platonic_Reader
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Alcibiades2 : ContentPage
	{
        private int sentenceNumber = 1;
        public string bookNumber = "AlcibiadesTwo";

		public Alcibiades2 ()
		{
            InitializeComponent();           
            sentence.FormattedText = CreateFormatedString(sentenceNumber, bookNumber);
		}

        private void OnNextButtonClick(object sender, EventArgs e)
        {
            sentenceNumber++;

            var formattedString = CreateFormatedString(sentenceNumber, bookNumber);

            if(sentenceNumber > 1)
            {
                previous.IsVisible = true;
            }

            sentence.FormattedText = formattedString;
            //textLayout.Children.Add(new Label { FormattedText = formattedString });
        }

        private void OnPreviousButtonClick(object sender, EventArgs e)
        {           
            sentenceNumber--;
            var formattedString = CreateFormatedString(sentenceNumber, bookNumber);

            if(sentenceNumber < 2)
            {
                previous.IsVisible = false;
            }

            sentence.FormattedText = formattedString;
            //textLayout.Children.Add(new Label { FormattedText = formattedString });
        }

        public FormattedString CreateFormatedString(int sentenceNumber, string bookNumber)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            //var layout = new StackLayout { Padding = new Thickness(5, 10) };
            var formattedString = new FormattedString();

            var fullSentence = Utilities.SentenceConstructor(sentenceNumber.ToString(), bookNumber);

            foreach (var item in fullSentence)
            {
                string humanReadableGrammarDescription = Utilities.ParseInterpreter(item.parseInfo);
                var span = new Span { Text = $"{item.item} ", ForegroundColor = Color.FromHex("#292b29"), FontFamily = "GFSBaskerville.ttf#GFS Porson", FontSize = 30 };
                span.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(async () => await DisplayAlert("GRAMMATICAL DESCRIPTION", humanReadableGrammarDescription, "OK")) });
                formattedString.Spans.Add(span);
            }
            return formattedString;
        }        
    }
}