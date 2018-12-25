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
            sentenceIndicator.Text = sentenceNumber.ToString();
            sentence.FormattedText = CreateFormatedString(sentenceNumber, bookNumber);
            page.Children.Add(CreateDictionaryEntries(sentenceNumber, bookNumber));
        }

        private void OnNextButtonClick(object sender, EventArgs e)
        {
            sentenceNumber++;

            var formattedString = CreateFormatedString(sentenceNumber, bookNumber);

            if (sentenceNumber > 1)
            {
                previous.TextColor = Color.FromHex("#292b29");
                previous.IsEnabled = true;
            }
            sentenceIndicator.Text = sentenceNumber.ToString();
            sentence.FormattedText = formattedString;
            page.Children.RemoveAt(2);
            page.Children.Add(CreateDictionaryEntries(sentenceNumber, bookNumber));
        }

        private void OnPreviousButtonClick(object sender, EventArgs e)
        {
            sentenceNumber--;
            var formattedString = CreateFormatedString(sentenceNumber, bookNumber);

            if (sentenceNumber < 2)
            {
                previous.TextColor = Color.FromHex("#999999");
                previous.IsEnabled = false;
            }

            sentenceIndicator.Text = sentenceNumber.ToString();
            sentence.FormattedText = formattedString;
            page.Children.RemoveAt(2);
            page.Children.Add(CreateDictionaryEntries(sentenceNumber, bookNumber));
        }

        private StackLayout CreateDictionaryEntries(int sentenceNumber, string bookNumber)
        {
            var fullSentence = Utilities.SentenceConstructor(sentenceNumber.ToString(), bookNumber);
            var dictionaryEntries = new StackLayout()
            {
                Margin = new Thickness(50, 0),
            };

            foreach (var item in fullSentence)
            {
                Label label;
                if (item.lemma != "," && item.lemma != "·")
                {
                    label = new Label { Text = $"{item.lemma}", TextColor = Color.Red, FontSize = 20, FontFamily = "GFSBaskerville.ttf#GFS Porson" };
                    dictionaryEntries.Children.Add(label);
                }
            }
            return dictionaryEntries;
        }

        public FormattedString CreateFormatedString(int sentenceNumber, string bookNumber)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            var formattedString = new FormattedString();

            var fullSentence = Utilities.SentenceConstructor(sentenceNumber.ToString(), bookNumber);

            foreach (var item in fullSentence)
            {
                string humanReadableGrammarDescription = Utilities.ParseInterpreter(item.parseInfo);
                var span = new Span
                {
                    Text = $"{item.item} ",
                    ForegroundColor = Color.FromHex("#292b29"),
                    FontSize = 30,
                    FontFamily = "GFSBaskerville.ttf#GFS Porson"
                };
                span.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(async () => await DisplayAlert("GRAMMATICAL DESCRIPTION", humanReadableGrammarDescription, "OK")) });

                formattedString.Spans.Add(span);
            }
            return formattedString;
        }
    }
}