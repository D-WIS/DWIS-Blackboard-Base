using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DWIS.Vocabulary.Development;
using DWIS.Vocabulary.Schemas;
using DWIS.Vocabulary.Utils;
namespace DWIS.API.DTO.ManifestFilesGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            string folder = FindDWISFolder() + @"\DDHub-Semantic-Interoperability\docs\vocabulary_development\examples";

            string manifestFolder = FindDWISFolder() + @"\DDHub-DSID-WP2-Common\docs\json\";


            GenerateManifestFiles(folder, manifestFolder);
        }

        private static string FindDWISFolder()
        {
            string dwis = "D-WIS";
            string dir = System.IO.Directory.GetCurrentDirectory();
            if (dir.Contains(dwis))
            {
                int idx = dir.IndexOf(dwis);
                return dir.Remove(idx + dwis.Length, dir.Length - idx - dwis.Length);

            }
            return string.Empty;
        }

        public static void GenerateManifestFiles(   string mdFolder, string manifestFolder)
        {
            var vocabulary = DWIS.Vocabulary.Standard.VocabularyProvider.Vocabulary;

            var files = System.IO.Directory.GetFiles(mdFolder).Where(f => f.EndsWith(".md"));

            foreach (var file in files)
            {
                if (VocabularyParsing.FromMDFile(file, vocabulary, out DWISInstance instance))
                {
                    DWISInstance.ToJsonFile(instance,  file.Replace(".md", ".json"));

                    var manifest = GenerateManifestFile(instance);

                    string manifestFileName = manifestFolder + System.IO.Path.GetFileNameWithoutExtension(file) + ".json";

                   System.IO.File.WriteAllText(manifestFileName, ManifestFile.ToJsonString(manifest));


                }
            }
        }

        private bool IsDrillingDataPoint(Noun n, DWISVocabulary vocabulary)
        {
            if (n.Name == Nouns.DWISNoun)
            {
                return false;
            }
            else if (n.Name == Nouns.DrillingDataPoint)
            {
                return false;
            }
            else return IsDrillingDataPoint(n.ParentNounName, vocabulary);
        }

        private static bool IsDrillingDataPoint(string n, DWISVocabulary vocabulary)
        {
            if (n == Nouns.DWISNoun)
            {
                return false;
            }
            else if (n == Nouns.DrillingDataPoint)
            {
                return true;
            }
            else return IsDrillingDataPoint(vocabulary.Nouns.Find(noun => noun.Name == n).ParentNounName, vocabulary);
        }

        private static ManifestFile GenerateManifestFile(DWISInstance instance)
        {
            
            ManifestFile manifestFile = new ManifestFile() 
            { 
                        InjectedVariables = new List<InjectedVariable>(),
                    InjectedNodes = new List<InjectedNode>(),
                    InjectedReferences = new List<InjectedReference>(),
            Provider =new InjectionProvider() { Name = instance.Name },
            InjectionInformation = new InjectionInformation() { InjectedNodesNamespaceAlias = "Nodes", InjectedVariablesNamespaceAlias = "Variables" }
            };



            foreach (var n in instance.Population)
            {
                if (n.TypeName == Nouns.DynamicDrillingSignal)
                {
                    manifestFile.InjectedVariables.Add(
                        new InjectedVariable()
                        {
                            InjectedName = n.Name,
                            NativeAddressSpaceNameSpace = manifestFile.InjectionInformation.InjectedVariablesNamespaceAlias,
                            NativeAddressSpaceNodeID = n.Name
                        });

                }
                else
                {
                    if (n.TypeName != Nouns.DrillingDataPoint)
                    {
                        if (IsDrillingDataPoint(n.TypeName, instance.Vocabulary))
                        {
                            manifestFile.InjectedNodes.Add(new InjectedNode() { UniqueName = n.Name, BrowseName = n.Name, DisplayName = n.Name, TypeDictionaryURI = "http://ddhub.no/" + Nouns.DrillingDataPoint });
                            manifestFile.InjectedReferences.Add(new InjectedReference()
                            {
                                Object = new NodeIdentifier() { ID = n.TypeName, NameSpace = "http://ddhub.no/" },
                                Subject = new NodeIdentifier() { ID = n.Name, NameSpace = manifestFile.InjectionInformation.InjectedNodesNamespaceAlias },
                                VerbURI = "http://ddhub.no/" + Verbs.BelongsToClass
                            });
                        }
                        else
                        {
                            manifestFile.InjectedNodes.Add(
                                 new InjectedNode()
                                 {
                                     UniqueName = n.Name,
                                     DisplayName = n.Name,
                                     BrowseName = n.Name,
                                     TypeDictionaryURI = "http://ddhub.no/" + n.TypeName
                                 });
                        }                    
                    }
                    else
                    {
                        manifestFile.InjectedNodes.Add(
                             new InjectedNode()
                             {
                                 UniqueName = n.Name,
                                 DisplayName = n.Name,
                                 BrowseName = n.Name,
                                 TypeDictionaryURI = "http://ddhub.no/" + n.TypeName
                             });
                    }
                }
            }


            foreach (var r in instance.Sentences)
            {
                manifestFile.InjectedReferences.Add(new InjectedReference() 
                {
                 Object = new NodeIdentifier() { ID = r.Object, NameSpace = manifestFile.InjectionInformation.InjectedNodesNamespaceAlias },
                 Subject = new NodeIdentifier() { ID = r.Subject, NameSpace = manifestFile.InjectionInformation.InjectedNodesNamespaceAlias },
                 VerbURI = "http://ddhub.no/" + r.Verb
                });
            }


            return manifestFile;
        }
    }
}
