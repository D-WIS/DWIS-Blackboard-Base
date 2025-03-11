using System;
using System.Collections.Generic;
using System.Text;
using DWIS.Vocabulary.Development;

namespace DWIS.OPCUA.Vocabulary
{
    public static class OPCSchemaWriter
    {
        public static void WriteFields(DWISVocabulary vocabulary, VocabularyOPCUAConfiguration configuration, string fileName)
        {
            configuration.ToTrees(out Tree<Noun> nounTree, out Tree<Noun> classTree, out Tree<Verb> verbTree, vocabulary);

            List<string> treatedFields = new List<string>();

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("namespace DWIS.OPCUA.Schemas");
            builder.AppendLine("{");
            builder.AppendLine("public static partial class Fields");
            builder.AppendLine("{");

            AddFieldElement(builder, nounTree, treatedFields);


            builder.AppendLine("}");
            builder.AppendLine("}");

            System.IO.File.WriteAllText(fileName, builder.ToString());

        }
        public static void WriteTypes(DWISVocabulary vocabulary, VocabularyOPCUAConfiguration configuration, string fileName)
        {
            configuration.ToTrees(out Tree<Noun> nounTree, out Tree<Noun> classTree, out Tree<Verb> verbTree, vocabulary);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("namespace DWIS.OPCUA.Schemas");
            builder.AppendLine("{");
            builder.AppendLine("public static partial class TypesIds");
            builder.AppendLine("{");

            AddElement(builder, nounTree, TypeSuffix);

            builder.AppendLine("}");
            builder.AppendLine("}");

            System.IO.File.WriteAllText(fileName, builder.ToString());
        }

        public static void WriteClasses(DWISVocabulary vocabulary, VocabularyOPCUAConfiguration configuration, string fileName)
        {
            configuration.ToTrees(out Tree<Noun> nounTree, out Tree<Noun> classTree, out Tree<Verb> verbTree, vocabulary);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("namespace DWIS.OPCUA.Schemas");
            builder.AppendLine("{");
            builder.AppendLine("public static partial class ClassDictionaryEntries");
            builder.AppendLine("{");

            AddElement(builder, classTree,DictionaryEntrySuffix);

            builder.AppendLine("}");
            builder.AppendLine("}");
            System.IO.File.WriteAllText(fileName, builder.ToString());
        }

        public static void WriteReferences(DWISVocabulary vocabulary, VocabularyOPCUAConfiguration configuration, string fileName)
        {
            configuration.ToTrees(out Tree<Noun> nounTree, out Tree<Noun> classTree, out Tree<Verb> verbTree, vocabulary);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("namespace DWIS.OPCUA.Schemas");
            builder.AppendLine("{");
            builder.AppendLine("public static partial class ReferencesIds");
            builder.AppendLine("{");

            AddElement(builder, verbTree, ReferenceTypeSuffix);

            builder.AppendLine("}");
            builder.AppendLine("}");
            System.IO.File.WriteAllText(fileName, builder.ToString());
        }


        private static string TypeSuffix = "Type";
        private static string ReferenceTypeSuffix = "ReferenceType";
        private static string DictionaryEntrySuffix = "DictionaryEntry";

        private static void AddFieldElement(StringBuilder stringBuilder, Tree<Noun> tree, List<string> treatedFields)
        {
            if (tree != null && tree.RootItem != null)
            {
                if (tree.RootItem.NounAttributes != null)
                {
                    foreach (var na in tree.RootItem.NounAttributes)
                    {
                        if (!treatedFields.Contains(na.Name))
                        {
                            string l = $"\tpublic static string {na.Name} = \"{na.Name}\";";
                            stringBuilder.AppendLine(l);
                            l = $"\tpublic static string {na.Name}attributeType = \"{na.Type}\";";
                            stringBuilder.AppendLine(l);
                            treatedFields.Add(na.Name);
                        }
                    }
                }
                if (tree.Children != null)
                {
                    foreach (var n in tree.Children)
                    {
                        AddFieldElement(stringBuilder, n,treatedFields);
                    }
                }
            }
        }



        private static void AddElement(StringBuilder stringBuilder, Tree<Noun> tree, string suffix)
        {
            if (tree != null && tree.RootItem != null)
            {
                if (tree.RootItem.Name != "Root class")
                {
                    string l = $"\tpublic static string {tree.RootItem} = \"{tree.RootItem}{suffix}\";";
                    stringBuilder.AppendLine(l);
                }
                if (tree.Children != null)
                {
                    foreach (var n in tree.Children)
                    {
                        AddElement(stringBuilder, n, suffix);
                    }
                }
            }
        }

        private static void AddElement(StringBuilder stringBuilder, Tree<Verb> tree, string suffix)
        {
            if (tree != null && tree.RootItem != null)
            {
                string l = $"\tpublic static string {tree.RootItem} = \"{tree.RootItem}{suffix}\";";
                stringBuilder.AppendLine(l);
                if (tree.Children != null)
                {
                    foreach (var n in tree.Children)
                    {
                        AddElement(stringBuilder, n, suffix);
                    }
                }
            }
        }

    }
}
