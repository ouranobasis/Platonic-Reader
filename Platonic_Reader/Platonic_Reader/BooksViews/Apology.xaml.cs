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
	public partial class Apology : ContentPage
	{
        private int sentenceNumber = 1;
        public string bookNumber = "Apology";

		public Apology ()
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

            if(sentenceNumber > 1)
            {
                previous.TextColor = Color.FromHex("#292b29");
                previous.IsEnabled = true;
            }
            sentenceIndicator.Text = sentenceNumber.ToString();
            sentence.FormattedText = formattedString;
            page.Children.RemoveAt(0);
            page.Children.Add(CreateDictionaryEntries(sentenceNumber, bookNumber));
        }

        private void OnPreviousButtonClick(object sender, EventArgs e)
        {           
            sentenceNumber--;
            var formattedString = CreateFormatedString(sentenceNumber, bookNumber);

            if(sentenceNumber < 2)
            {
                previous.TextColor = Color.FromHex("#999999");
                previous.IsEnabled = false;
            }

            sentenceIndicator.Text = sentenceNumber.ToString();
            sentence.FormattedText = formattedString;
            page.Children.RemoveAt(0);
            page.Children.Add(CreateDictionaryEntries(sentenceNumber, bookNumber));
        }

        private StackLayout CreateDictionaryEntries(int sentenceNumber, string bookNumber)
        {
            var fullSentence = Utilities.SentenceConstructor(sentenceNumber.ToString(), bookNumber);
            var dictionaryEntries = new StackLayout()
            {
                Margin = new Thickness(50, 0),
            };

            int wordNumber = 1;
            foreach (var item in fullSentence)
            {
                Label label;
                Label definitionLabel;

                if (item.lemma != "," && item.lemma != "·" && item.lemma != "." && item.lemma != ";")
                {
                    label = new Label { Text = $" {wordNumber}. {item.lemma}",  TextColor = Color.FromHex("#303030"), FontSize = 20, FontFamily = "GFSBaskerville.ttf#GFS Porson" };
                    definitionLabel = new Label { Text = $"- {item.definition}.", Margin= new Thickness(20,0,0,0), TextColor = Color.FromHex("#303030"), FontSize = 15, FontFamily = "Crimson-Italic.ttf#Crimson" }; 
                    dictionaryEntries.Children.Add(label);
                    dictionaryEntries.Children.Add(definitionLabel);
                }
                wordNumber++;
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
                var span = new Span {
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