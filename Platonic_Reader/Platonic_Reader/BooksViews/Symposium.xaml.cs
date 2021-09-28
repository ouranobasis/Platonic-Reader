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
	public partial class Symposium : ContentPage
	{
        private int sentenceNumber = 1;
        public string bookNumber = "Symposium";

		public Symposium ()
        {
            InitializeComponent();
            sentenceIndicator.Text = sentenceNumber.ToString();
            sentence.FormattedText = CreateFormatedString(sentenceNumber, bookNumber);
            page.Children.Add(CreateDictionaryEntries(sentenceNumber, bookNumber));
            previous.IsEnabled = false;
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
            page.Children.RemoveAt(0);
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
            page.Children.RemoveAt(0);
            page.Children.Add(CreateDictionaryEntries(sentenceNumber, bookNumber));
        }

        private void DismissModal(object sender, EventArgs e)
        {
            popupLoginView.IsVisible = false;
        }

        private Grid CreateDictionaryEntries(int sentenceNumber, string bookNumber)
        {
            var fullSentence = Utilities.SentenceConstructor(sentenceNumber.ToString(), bookNumber);
            var dictionaryEntriesOne = new StackLayout()
            {
                Margin = new Thickness(45, 0, 0, 0),
            };

            var dictionaryEntriesTwo = new StackLayout();

            var dictionaryColumns = new Grid();

            dictionaryColumns.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            dictionaryColumns.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            int wordNumber = 1;
            foreach (var item in fullSentence)
            {
                Label label;

                if (item.lemma != "," && item.lemma != "·" && item.lemma != "." && item.lemma != ";" && item.lemma != "—" && item.lemma != "’" && item.lemma != "‘")
                {
                    label = new Label { Text = $" {wordNumber}. {item.lemma}", TextColor = Color.FromHex("#303030"), FontSize = 20, FontFamily = "GFSBaskerville.ttf#GFS Porson" };
                    label.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() => { popupLoginView.IsVisible = true; modalTextContent.Text = Utilities.CallDictionaryDefinition(item.lemma); modalTextContent.FontFamily = "Crimson-Italic.ttf#Crimson"; modalLemma.Text = item.lemma; modalTitle.Text = "DICTIONARY"; })
                    });
                    if (wordNumber <= fullSentence.Count / 2)
                    {
                        dictionaryEntriesOne.Children.Add(label);
                        dictionaryColumns.Children.Add(dictionaryEntriesOne, 0, 0);
                    }
                    else if (wordNumber > fullSentence.Count / 2)
                    {
                        dictionaryEntriesTwo.Children.Add(label);
                        dictionaryColumns.Children.Add(dictionaryEntriesTwo, 1, 0);
                    }
                }
                wordNumber++;
            }
            return dictionaryColumns;
        }

        public FormattedString CreateFormatedString(int sentenceNumber, string bookNumber)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            var formattedString = new FormattedString();

            var fullSentence = Utilities.SentenceConstructor(sentenceNumber.ToString(), bookNumber);

            string spaceCharacter = "start value";
            var span = new Span();

            foreach (var item in fullSentence)
            {
                string humanReadableGrammarDescription = Utilities.ParseInterpreter(item.parseInfo);

                if (spaceCharacter != "start value" && item.item != "," && item.item != "." && item.item != ";" && item.item != "—" && item.item != "·" && item.item != ":")
                {
                    spaceCharacter = " ";
                }

                else
                {
                    spaceCharacter = "";
                }

                span = new Span
                {
                    Text = $"{spaceCharacter}{item.item}",
                    ForegroundColor = Color.FromHex("#292b29"),
                    FontSize = 30,
                    FontFamily = "GFSBaskerville.ttf#GFS Porson"
                };

                span.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => { popupLoginView.IsVisible = true; modalTextContent.Text = $"{humanReadableGrammarDescription}"; modalLemma.Text = item.item; modalTitle.Text = "MORPHOLOGY"; })
                    //Command = new Command(async () => await DisplayAlert("GRAMMATICAL DESCRIPTION", $"{humanReadableGrammarDescription}", "OK"))
                });

                formattedString.Spans.Add(span);
            }
            return formattedString;
        }
    }
}