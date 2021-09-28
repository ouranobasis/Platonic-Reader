using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

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

        public static string CallDictionaryDefinition(string greekWord)
        {
            string definition = "cant find it";

            if (greekWord !="," && greekWord != "·" && greekWord != "." && greekWord != ";" && greekWord != "—")
            { 
                var assembly = typeof(MainPage).GetTypeInfo().Assembly;
                var stream = assembly.GetManifestResourceStream($"Platonic_Reader.Dictionary.{GetDictionarySegmentFile(greekWord)}");
                int count = 0;

                using (XmlReader reader = XmlReader.Create(stream))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && reader.Name == "Greek-Entry" && reader.GetAttribute("key") == greekWord)
                        {
                            definition = "";
                            reader.ReadToDescendant("English-Entry");                            
                            do
                            {
                                definition += $"{reader.ReadString()}\n";
                                count++;
                            } while (reader.ReadToNextSibling("English-Entry"));
                        }
                    }
                }
            }
            return definition;
        }

        public static string GetDictionaryDefinition(XmlReader definitionReader)
        {
            string definition = "";
            while (definitionReader.Read())
            {
                if(definitionReader.IsStartElement() && definitionReader.Name == "English-Entry")
                {
                    definitionReader.Read();
                    
                    //definition = definitionReader.Value.Trim();
                    definition = definitionReader.Name;
                }
            }
            return definition;
        }

        static string GetDictionarySegmentFile(string greekWord)
        {

            var firstLetter = greekWord[0];

            var xmlFileTitle = "";
            switch (firstLetter)
            {
                case 'α':
                    xmlFileTitle = @"alpha";
                    break;
                case 'ἀ':
                    xmlFileTitle = @"alpha";
                    break;
                case 'ἁ':
                    xmlFileTitle = @"alpha";
                    break;
                case 'ἃ':
                    xmlFileTitle = @"alpha";
                    break;
                case 'ἂ':
                    xmlFileTitle = @"alpha";
                    break;
                case 'ἅ':
                    xmlFileTitle = @"alpha";
                    break;
                case 'ἄ':
                    xmlFileTitle = @"alpha";
                    break;
                case 'Ἁ':
                    xmlFileTitle = @"alpha";
                    break;
                case 'Ἀ':
                    xmlFileTitle = @"alpha";
                    break;
                case 'β':
                    xmlFileTitle = @"beta";
                    break;
                case 'Β':
                    xmlFileTitle = @"beta";
                    break;
                case 'γ':
                    xmlFileTitle = @"gamma";
                    break;
                case 'Γ':
                    xmlFileTitle = @"gamma";
                    break;
                case 'δ':
                    xmlFileTitle = @"delta";
                    break;
                case 'Δ':
                    xmlFileTitle = @"delta";
                    break;
                case 'Ε':
                    xmlFileTitle = @"epsilon";
                    break;
                case 'Ἑ':
                    xmlFileTitle = @"epsilon";
                    break;
                case 'Ἐ':
                    xmlFileTitle = @"epsilon";
                    break;
                case 'ε':
                    xmlFileTitle = @"epsilon";
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
                case 'ἕ':
                    xmlFileTitle = @"epsilon";
                    break;
                case 'ἓ':
                    xmlFileTitle = @"epsilon";
                    break;
                case 'ἒ':
                    xmlFileTitle = @"epsilon";
                    break;
                case 'ζ':
                    xmlFileTitle = @"zeta";
                    break;
                case 'Ζ':
                    xmlFileTitle = @"zeta";
                    break;
                case 'Η':
                    xmlFileTitle = @"eta";
                    break;
                case 'Ἡ':
                    xmlFileTitle = @"eta";
                    break;
                case 'Ἠ':
                    xmlFileTitle = @"eta";
                    break;
                case 'η':
                    xmlFileTitle = @"eta";
                    break;
                case 'ἡ':
                    xmlFileTitle = @"eta";
                    break;
                case 'ἠ':
                    xmlFileTitle = @"eta";
                    break;
                case 'ἤ':
                    xmlFileTitle = @"eta";
                    break;
                case 'ἥ':
                    xmlFileTitle = @"eta";
                    break;
                case 'ἣ':
                    xmlFileTitle = @"eta";
                    break;
                case 'ἢ':
                    xmlFileTitle = @"eta";
                    break;
                case 'ἦ':
                    xmlFileTitle = @"eta";
                    break;
                case 'ἧ':
                    xmlFileTitle = @"eta";
                    break;
                case 'ᾖ':
                    xmlFileTitle = @"eta";
                    break;
                case 'ᾗ':
                    xmlFileTitle = @"eta";
                    break;
                case 'ᾔ':
                    xmlFileTitle = @"eta";
                    break;
                case 'ᾒ':
                    xmlFileTitle = @"eta";
                    break; 
                case 'θ':
                    xmlFileTitle = @"eta";
                    break;
                case 'Θ':
                    xmlFileTitle = @"eta";
                    break;
                case 'ι':
                    xmlFileTitle = @"iota";
                    break;
                case 'Ι':
                    xmlFileTitle = @"iota";
                    break;
                case 'Ἱ':
                    xmlFileTitle = @"iota";
                    break;
                case 'Ἰ':
                    xmlFileTitle = @"iota";
                    break;
                case 'ἵ':
                    xmlFileTitle = @"iota";
                    break;
                case 'ἴ':
                    xmlFileTitle = @"iota";
                    break;
                case 'ἰ':
                    xmlFileTitle = @"iota";
                    break;
                case 'ἱ':
                    xmlFileTitle = @"iota";
                    break;
                case 'κ':
                    xmlFileTitle = @"kappa";
                    break;
                case 'Κ':
                    xmlFileTitle = @"kappa";
                    break;
                case 'λ':
                    xmlFileTitle = @"lambda";
                    break;
                case 'Λ':
                    xmlFileTitle = @"lambda";
                    break;
                case 'μ':
                    xmlFileTitle = @"mu";
                    break;
                case 'Μ':
                    xmlFileTitle = @"mu";
                    break;
                case 'ν':
                    xmlFileTitle = @"nu";
                    break;
                case 'Ν':
                    xmlFileTitle = @"nu";
                    break;
                case 'ξ':
                    xmlFileTitle = @"xsi";
                    break;
                case 'Ξ':
                    xmlFileTitle = @"xsi";
                    break;
                case 'Ο':
                    xmlFileTitle = @"omicron";
                    break;
                case 'Ὁ':
                    xmlFileTitle = @"omicron";
                    break;
                case 'Ὀ':
                    xmlFileTitle = @"omicron";
                    break;
                case 'ο':
                    xmlFileTitle = @"omicron";
                    break;
                case 'ὁ':
                    xmlFileTitle = @"omicron";
                    break;
                case 'ὀ':
                    xmlFileTitle = @"omicron";
                    break;
                case 'ὃ':
                    xmlFileTitle = @"omicron";
                    break;
                case 'ὂ':
                    xmlFileTitle = @"omicron";
                    break;
                case 'ὅ':
                    xmlFileTitle = @"omicron";
                    break;
                case 'ὄ':
                    xmlFileTitle = @"omicron";
                    break;
                case 'π':
                    xmlFileTitle = @"pi";
                    break;
                case 'Π':
                    xmlFileTitle = @"pi";
                    break;
                case 'Ρ':
                    xmlFileTitle = @"rho";
                    break;
                case 'Ῥ':
                    xmlFileTitle = @"rho";
                    break;
                case 'ρ':
                    xmlFileTitle = @"rho";
                    break;
                case 'ῥ':
                    xmlFileTitle = @"rho";
                    break;
                case 'Σ':
                    xmlFileTitle = @"sigma";
                    break;
                case 'σ':
                    xmlFileTitle = @"sigma";
                    break;
                case 'τ':
                    xmlFileTitle = @"tau";
                    break;
                case 'Τ':
                    xmlFileTitle = @"tau";
                    break;
                case 'Υ':
                    xmlFileTitle = @"upsilon";
                    break;
                case 'Ὑ':
                    xmlFileTitle = @"upsilon";
                    break;
                case 'υ':
                    xmlFileTitle = @"upsilon";
                    break;
                case 'ὑ':
                    xmlFileTitle = @"upsilon";
                    break;
                case 'ὐ':
                    xmlFileTitle = @"upsilon";
                    break;
                case 'ὓ':
                    xmlFileTitle = @"upsilon";
                    break;
                case 'ὒ':
                    xmlFileTitle = @"upsilon";
                    break;
                case 'ὕ':
                    xmlFileTitle = @"upsilon";
                    break;
                case 'ὔ':
                    xmlFileTitle = @"upsilon";
                    break;
                case 'ὗ':
                    xmlFileTitle = @"upsilon";
                    break;
                case 'ὖ':
                    xmlFileTitle = @"upsilon";
                    break;
                case 'φ':
                    xmlFileTitle = @"phi";
                    break;
                case 'Φ':
                    xmlFileTitle = @"phi";
                    break;
                case 'ψ':
                    xmlFileTitle = @"psi";
                    break;
                case 'Ψ':
                    xmlFileTitle = @"psi";
                    break;
                case 'χ':
                    xmlFileTitle = @"chi";
                    break;
                case 'Χ':
                    xmlFileTitle = @"chi";
                    break;
                case 'Ω':
                    xmlFileTitle = @"omega";
                    break;
                case 'Ὡ':
                    xmlFileTitle = @"omega";
                    break;
                case 'Ὠ':
                    xmlFileTitle = @"omega";
                    break;
                case 'ω':
                    xmlFileTitle = @"omega";
                    break;
                case 'ὡ':
                    xmlFileTitle = @"omega";
                    break;
                case 'ὠ':
                    xmlFileTitle = @"omega";
                    break;
                case 'ὣ':
                    xmlFileTitle = @"omega";
                    break;
                case 'ὢ':
                    xmlFileTitle = @"omega";
                    break;
                case 'ὥ':
                    xmlFileTitle = @"omega";
                    break;
                case 'ὤ':
                    xmlFileTitle = @"omega";
                    break;
                case 'ὧ':
                    xmlFileTitle = @"omega";
                    break;
                case 'ὦ':
                    xmlFileTitle = @"omega";
                    break;
            }

            xmlFileTitle = $"{xmlFileTitle}.xml";

            return xmlFileTitle;
        }

    }
}

