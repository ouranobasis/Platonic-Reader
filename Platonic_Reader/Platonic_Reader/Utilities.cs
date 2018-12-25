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

                            var sentenceInfo = WordReader(reader.ReadSubtree());

                            sentenceItem.item = sentenceInfo[0];
                            sentenceItem.lemma = sentenceInfo[1];
                            fullSentence.Add(sentenceItem);

                        } while (reader.ReadToNextSibling("t"));
                    }
                }
            }
            return fullSentence;
        }

        public static string[] WordReader(XmlReader wordReader)
        {
            string[] word = new string[2];
            while (wordReader.Read())
            {
                if (wordReader.IsStartElement() && wordReader.Name == "f")
                {
                    wordReader.Read();
                    word[0] = wordReader.Value.Trim();
                }
                if (wordReader.IsStartElement() && wordReader.Name == "l")
                {
                    wordReader.Read();
                    if (wordReader.IsStartElement() && !wordReader.IsEmptyElement)
                    {
                        wordReader.Read();
                        word[1] = wordReader.Value.Trim();
                    }
                    else
                        {
                            word[1] = word[0];
                        }
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

        public string DictionaryDefinition(string greekWord)
        {
               

            string definition = "";
            return definition;
        }

        static string GetDictionarySegmentFile(string greekWord)
        {

            var firstLetter = greekWord[0];

            var xmlFileTitle = "";
            switch (firstLetter)
            {
                case 'ἀ':
                    xmlFileTitle = @"alpha";
                    break;
                case 'ἁ':
                    xmlFileTitle = @"alpha";
                    break;
                case 'ἃ':
                    xmlFileTitle = @"alpha";
                    break;
                case 'ἄ':
                    xmlFileTitle = @"alpha";
                    break;
                case 'β':
                    xmlFileTitle = @"beta";
                    break;
                case 'γ':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse C.xml";
                    break;
                case 'δ':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse D.xml";
                    break;
                case 'ἐ':
                    xmlFileTitle = @"epsilon";
                    break;
                case 'ἑ':
                    xmlFileTitle = @"epsilon";
                    break;
                case 'ἔ':
                    xmlFileTitle = @"epsilon";
                    break;
                case 'ζ':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse F.xml";
                    break;
                case 'ἡ':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse G.xml";
                    break;
                case 'ἤ':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse G.xml";
                    break;
                case 'ἦ':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse G.xml";
                    break;
                case 'ἧ':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse H.xml";
                    break;
                case 'ἧ':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse H.xml";
                    break;
                case 'i':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse I.xml";
                    break;
                case 'j':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse J.xml";
                    break;
                case 'k':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse K.xml";
                    break;
                case 'l':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse L.xml";
                    break;
                case 'm':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse M.xml";
                    break;
                case 'n':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse N.xml";
                    break;
                case 'o':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse O.xml";
                    break;
                case 'p':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse P.xml";
                    break;
                case 'q':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse Q.xml";
                    break;
                case 'r':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse R.xml";
                    break;
                case 's':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse S.xml";
                    break;
                case 't':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse T.xml";
                    break;
                case 'u':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse U.xml";
                    break;
                case 'v':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse V.xml";
                    break;
                case 'w':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse W.xml";
                    break;
                case 'y':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse Y.xml";
                    break;
                case 'z':
                    xmlFileTitle = @"C:\Users\IWANOS\Source\Repos\Woodhouse-Dictionary\Woodhouse Dictionary\Woodhouse XML\Woodhouse Z.xml";
                    break;
            }
            return xmlFileTitle;
        }

    }
}

