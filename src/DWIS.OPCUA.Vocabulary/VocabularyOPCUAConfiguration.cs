using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWIS.Vocabulary.Development;

namespace DWIS.OPCUA.Vocabulary
{
    public class VocabularyOPCUAConfiguration
    {
        public NounOPCUAConfiguration[] NounOPCUAConfigurations { get; set; }
        public VerbOPCUAConfiguration[] VerbOPCUAConfigurations { get; set; }

        public NounOPCUAConfiguration Find(Noun noun)
        {
            return NounOPCUAConfigurations?.FirstOrDefault(c => c.Noun.Name.Equals(noun.Name));
        }

        public NounOPCUAConfiguration FindNounConfiguration(string nounName, bool ignoreCase = false)
        {
            if (ignoreCase)
            {
                return NounOPCUAConfigurations?.FirstOrDefault(c => c.Noun.Name.ToLower().Equals(nounName.ToLower()));
            }
            else
            {
                return NounOPCUAConfigurations?.FirstOrDefault(c => c.Noun.Name.Equals(nounName));
            }
         
        }

        public VerbOPCUAConfiguration Find(Verb verb)
        {
            return VerbOPCUAConfigurations?.FirstOrDefault(c => c.Verb.Name.Equals(verb.Name));
        }

        public VerbOPCUAConfiguration FindVerbConfiguration(string verbName, bool ignoreCase = false)
        {
            if (ignoreCase)
            {
                return VerbOPCUAConfigurations?.FirstOrDefault(c => c.Verb.Name.ToLower().Equals(verbName.ToLower()));
            }
            else
            {
                return VerbOPCUAConfigurations?.FirstOrDefault(c => c.Verb.Name.Equals(verbName));
            }
        }


        public bool CheckClasses(IList<Noun> shouldBeClasses)
        {
            bool ok = true;
            bool stop = false;
            while (!stop)
            {
                stop = true;
                for (int i = 0; i < NounOPCUAConfigurations.Length; i++)
                {
                    if (NounOPCUAConfigurations[i].ExportAsType)
                    {
                        var parent = FindNounConfiguration(NounOPCUAConfigurations[i].Noun.ParentNounName);
                        if (parent != null && !parent.ExportAsType)
                        {
                            ok = false;
                            stop = false;
                            shouldBeClasses.Add(NounOPCUAConfigurations[i].Noun);
                            NounOPCUAConfigurations[i].ExportAsType = false;
                        }
                    }
                }
            }
            return ok;
        }

        public void ToTrees(out Tree<Noun> types, out Tree<Noun> classes, out Tree<Verb> verbs, DWISVocabulary vocabulary)
        {
            vocabulary.ToTrees(out var nounTree, out var verbTree);

            types = nounTree;
            verbs = verbTree;
            Noun rootClass = new Noun() { Name = "Root class" };
            classes = new Tree<Noun>(rootClass);
            ExtractClasses(types, classes);
        }

        private bool ExtractClasses(Tree<Noun> types, Tree<Noun> classes)
        {
            var conf = Find(types.RootItem);
            if (conf!=null && !conf.ExportAsType)
            {
                classes.Children.Add(types);
                return true;
            }
            else
            {
                List<Tree<Noun>> toRemove = new List<Tree<Noun>>();
                foreach (var c in types.Children)
                {
                    if (ExtractClasses(c, classes))
                    {
                        toRemove.Add(c);
                    }
                }
                foreach (var c in toRemove)
                {
                    types.Children.Remove(c);
                }
                return false;
            }
        }

        //public static VocabularyOPCUAConfiguration GetDefaultOne(DWISVocabulary vocabulary)
        //{
        //    VocabularyOPCUAConfiguration configuration = new VocabularyOPCUAConfiguration();

        //    configuration.NounOPCUAConfigurations = new NounOPCUAConfiguration[vocabulary.Nouns.Count];
        //    configuration.VerbOPCUAConfigurations = new VerbOPCUAConfiguration[vocabulary.Verbs.Count];

        //    for (int i = 0; i < vocabulary.Nouns.Count; i++)
        //    {
        //        NounOPCUAConfiguration conf = new NounOPCUAConfiguration() { Noun = vocabulary.Nouns[i] };
        //        if (conf.Noun.ParentNounName == "DrillingDataPoint")
        //        {
        //            conf.ExportAsType = false;
        //        }
        //        else
        //        {
        //            conf.ExportAsType = true;
        //        }
        //        configuration.NounOPCUAConfigurations[i] = conf;
        //    }
        //    for (int i = 0; i < vocabulary.Verbs.Count; i++)
        //    {
        //        VerbOPCUAConfiguration conf = new VerbOPCUAConfiguration() { Verb = vocabulary.Verbs[i] };
        //        configuration.VerbOPCUAConfigurations[i] = conf;
        //    }
        //    return configuration;
        //}

        private static VocabularyOPCUAConfiguration FromJSONString(string text)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(text, typeof(VocabularyOPCUAConfiguration));
            if (obj != null && obj is VocabularyOPCUAConfiguration)
            {
                return (VocabularyOPCUAConfiguration)obj;
            }
            else return null;
        }
        public static VocabularyOPCUAConfiguration FromJSONFile(string fileName)
        {
            return FromJSONString(System.IO.File.ReadAllText(fileName));
        }
        public static string ToJSONString(VocabularyOPCUAConfiguration configuration)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(configuration);
        }
        public static void ToJSONFile(VocabularyOPCUAConfiguration configuration, string fileName)
        {
            System.IO.File.WriteAllText(fileName, ToJSONString(configuration));
        }

        public static VocabularyOPCUAConfiguration GetConfiguration()
        {
            return FromJSONString(Properties.Resources.opcuaConfiguration);
        }


    }

    public class NounOPCUAConfiguration
    {
        public Noun Noun { get; set; }

        public bool ExportAsType { get; set; }

        public override string ToString()
        {
            return Noun.DisplayName.ToString();
        }
    }

    public class VerbOPCUAConfiguration
    {
        public Verb Verb { get; set; }
        public string OPCUAParentReference { get; set; }
        public bool Export { get; set; } = true;
        public override string ToString()
        {
            return Verb.DisplayName.ToString();
        }
    }
}
