using System;

namespace DWIS.OPCUA.SchemaGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            var vocabulary = DWIS.Vocabulary.Standard.VocabularyProvider.Vocabulary;
            var conf = DWIS.OPCUA.Vocabulary.VocabularyOPCUAConfiguration.GetConfiguration();//.FromJSONFile(opcUAConfigurationFile);

            string typesFileName = FindDWISFolder() + @"\DDHub-DSID-WP2-Common\src\DWIS.Vocabulary.OPCUA\DWIS.OPCUA.Schemas\TypesIds.cs";
            string referencesFileName = FindDWISFolder() + @"\DDHub-DSID-WP2-Common\src\DWIS.Vocabulary.OPCUA\DWIS.OPCUA.Schemas\ReferencesIds.cs";
            string classesFileName = FindDWISFolder() + @"\DDHub-DSID-WP2-Common\src\DWIS.Vocabulary.OPCUA\DWIS.OPCUA.Schemas\ClassesIds.cs";
            string fieldsFileName = FindDWISFolder() + @"\DDHub-DSID-WP2-Common\src\DWIS.Vocabulary.OPCUA\DWIS.OPCUA.Schemas\Fields.cs";

            DWIS.OPCUA.Vocabulary.OPCSchemaWriter.WriteTypes(vocabulary, conf, typesFileName);
            DWIS.OPCUA.Vocabulary.OPCSchemaWriter.WriteClasses(vocabulary, conf, classesFileName);
            DWIS.OPCUA.Vocabulary.OPCSchemaWriter.WriteReferences(vocabulary, conf, referencesFileName);
            DWIS.OPCUA.Vocabulary.OPCSchemaWriter.WriteFields(vocabulary, conf, fieldsFileName);

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


    }
}
