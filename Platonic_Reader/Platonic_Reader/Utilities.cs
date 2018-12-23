using System;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platonic_Reader
{
    class Utilities
    {
        public static Stream GetResourceTextFile(string textName)
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            //var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()[1];
            var stream = assembly.GetManifestResourceStream($"Platonic_Reader.Texts.{textName}.xml");

            return stream;
        }

        public static List<SentenceItem> SentenceConstructor(string sentenceNumber, string textName)
        {
            var file = GetResourceTextFile(textName);

            List<SentenceItem> fullSentence = new List<SentenceItem>();

            using (XmlReader reader = XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement() && reader.Name == "s" && reader.GetAttribute("n") == sentenceNumber)
                    {
                        reader.ReadToDescendant("t");
                        do
                        {
                            SentenceItem sentenceItem = new SentenceItem();
                            sentenceItem.parseInfo = reader.GetAttribute("o");
                            sentenceItem.item = WordReader(reader.ReadSubtree());
                            fullSentence.Add(sentenceItem);

                        } while (reader.ReadToNextSibling("t"));

                    }
                }
            }
            return fullSentence;
        }

        public static string WordReader(XmlReader wordReader)
        {
            string word = "";
            while (wordReader.Read())
            {
                if (wordReader.IsStartElement() && wordReader.Name == "f")
                {
                    wordReader.Read();
                    word = wordReader.Value.Trim();
                }
            }
            return word;
        }

        public static string ParseInterpreter(string parseInfo)
        {
            parseInfo.ToArray();

            string interpretedCode = "";

            for (int i = 0; i < parseInfo.Count(); i++)
            {
                if (i == 0)
                    switch (parseInfo[i])
                    {
                        case 'n':
                            interpretedCode = "noun ";
                            break;
                        case 'v':
                            interpretedCode = "verb ";
                            break;
                        case 'a':
                            interpretedCode = "adjective ";
                            break;
                        case 'd':
                            interpretedCode = "adverb ";
                            break;
                        case 'l':
                            interpretedCode = "article ";
                            break;
                        case 'g':
                            interpretedCode = "particle ";
                            break;
                        case 'c':
                            interpretedCode = "conjuction ";
                            break;
                        case 'r':
                            interpretedCode = "preposition ";
                            break;
                        case 'p':
                            interpretedCode = "pronoun ";
                            break;
                        case 'm':
                            interpretedCode = "numeral ";
                            break;
                        case 'i':
                            interpretedCode = "interjection ";
                            break;
                        case 'u':
                            interpretedCode = "punctuation ";
                            break;
                    }
                if (i == 1)
                    switch (parseInfo[i])
                    {
                        case '1':
                            interpretedCode += "first person ";
                            break;
                        case '2':
                            interpretedCode += "second person ";
                            break;
                        case '3':
                            interpretedCode += "third person ";
                            break;
                    }
                if (i == 2)
                    switch (parseInfo[i])
                    {
                        case 's':
                            interpretedCode += "singular ";
                            break;
                        case 'p':
                            interpretedCode += "plural ";
                            break;
                        case 'd':
                            interpretedCode += "dual ";
                            break;
                    }
                if (i == 3)
                    switch (parseInfo[i])
                    {
                        case 'p':
                            interpretedCode += "present ";
                            break;
                        case 'i':
                            interpretedCode += "imperfect ";
                            break;
                        case 'r':
                            interpretedCode += "perfect ";
                            break;
                        case 'l':
                            interpretedCode += "pluperfect ";
                            break;
                        case 't':
                            interpretedCode += "future perfect ";
                            break;
                        case 'f':
                            interpretedCode += "future ";
                            break;
                        case 'a':
                            interpretedCode += "aorist ";
                            break;
                    }
                if (i == 4)
                    switch (parseInfo[i])
                    {
                        case 'i':
                            interpretedCode += "indicative ";
                            break;
                        case 's':
                            interpretedCode += "subjunctive ";
                            break;
                        case 'o':
                            interpretedCode += "operative ";
                            break;
                        case 'n':
                            interpretedCode += "infinitive ";
                            break;
                        case 'm':
                            interpretedCode += "imperative ";
                            break;
                        case 'p':
                            interpretedCode += "participle ";
                            break;
                    }
                if (i == 5)
                    switch (parseInfo[i])
                    {
                        case 'a':
                            interpretedCode += "active ";
                            break;
                        case 'p':
                            interpretedCode += "passive ";
                            break;
                        case 'm':
                            interpretedCode += "middle ";
                            break;
                        case 'e':
                            interpretedCode += "medio-passive ";
                            break;
                    }
                if (i == 6)
                    switch (parseInfo[i])
                    {
                        case 'm':
                            interpretedCode += "masculine ";
                            break;
                        case 'f':
                            interpretedCode += "feminine ";
                            break;
                        case 'n':
                            interpretedCode += "neuter ";
                            break;
                    }
                if (i == 7)
                    switch (parseInfo[i])
                    {
                        case 'n':
                            interpretedCode += "nominitive ";
                            break;
                        case 'g':
                            interpretedCode += "genitive ";
                            break;
                        case 'd':
                            interpretedCode += "dative ";
                            break;
                        case 'a':
                            interpretedCode += "accusative ";
                            break;
                        case 'v':
                            interpretedCode += "vocative ";
                            break;
                        case 'l':
                            interpretedCode += "locative ";
                            break;
                    }
                if (i == 8)
                    switch (parseInfo[i])
                    {
                        case 'c':
                            interpretedCode += "comparative ";
                            break;
                        case 's':
                            interpretedCode += "superlative ";
                            break;
                    }
            }
            return interpretedCode;
        }

        //private static List<TextItem> GetLibraryCollection()
        //{
        //    var assembly = typeof(MainPage).GetTypeInfo().Assembly;
        //    string[] resourceName = assembly.GetManifestResourceNames();
        //    List<TextItem> libraryOfTexts = new List<TextItem>();
        //    int increment = 0;

        //    foreach (var item in resourceName)
        //    {
        //        TextItem text = new TextItem();
        //        var stream = assembly.GetManifestResourceStream(item);

        //        using (XmlReader textName = XmlReader.Create(stream))
        //        {
        //            if (textName.IsStartElement() && textName.Name == "text")
        //                text.Author = (textName.GetAttribute("author") == "") ? "Anonymous" : textName.GetAttribute("author");
        //                text.Title = (textName.GetAttribute("title") == "") ? "Untitled" : textName.GetAttribute("title");
        //                text.Id = increment;
        //                text.FileName = item;
        //                increment++;
        //        }
        //        libraryOfTexts.Add(text);
        //    }
        //    return libraryOfTexts;
        //}
    }
}

