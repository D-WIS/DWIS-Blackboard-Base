using System;
namespace DWIS.API.ManifestFileSchemaGeneration
{
    class Program
    {
        static void Main(string[] args)
        {

            Newtonsoft.Json.Schema.Generation.JSchemaGenerator schemaGenerator = new Newtonsoft.Json.Schema.Generation.JSchemaGenerator();
            schemaGenerator.SchemaIdGenerationHandling = Newtonsoft.Json.Schema.Generation.SchemaIdGenerationHandling.TypeName;
            


            var schema = schemaGenerator.Generate(typeof(DWIS.API.DTO.ManifestFile));
            string schemaPath = @"..\..\..\..\..\..\docs\json\" + nameof(DTO.ManifestFile) + ".json";

            System.IO.TextWriter textWriter = new System.IO.StreamWriter(schemaPath);
           Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(textWriter);
            schema.WriteTo(writer);
            textWriter.Flush();
            textWriter.Dispose();

            schema = schemaGenerator.Generate(typeof(DTO.ManifestInjectionResult));
            schemaPath = @"..\..\..\..\..\..\docs\json\" + nameof(DTO.ManifestInjectionResult) + ".json";

             textWriter = new System.IO.StreamWriter(schemaPath);
             writer = new Newtonsoft.Json.JsonTextWriter(textWriter);
            schema.WriteTo(writer);
            textWriter.Flush();
            textWriter.Dispose();

            schema = schemaGenerator.Generate(typeof(DTO.QueryResult));
            schemaPath = @"..\..\..\..\..\..\docs\json\" + nameof(DTO.QueryResult) + ".json";

            textWriter = new System.IO.StreamWriter(schemaPath);
            writer = new Newtonsoft.Json.JsonTextWriter(textWriter);
            schema.WriteTo(writer);
            textWriter.Flush();
            textWriter.Dispose();

            schema = schemaGenerator.Generate(typeof(DTO.RegisteredQueriesDiff));
            schemaPath = @"..\..\..\..\..\..\docs\json\" + nameof(DTO.RegisteredQueriesDiff) + ".json";

            textWriter = new System.IO.StreamWriter(schemaPath);
            writer = new Newtonsoft.Json.JsonTextWriter(textWriter);
            schema.WriteTo(writer);
            textWriter.Flush();
            textWriter.Dispose();

            schema = schemaGenerator.Generate(typeof(DTO.QueryResultsDiff));
            schemaPath = @"..\..\..\..\..\..\docs\json\" + nameof(DTO.QueryResultsDiff) + ".json";

            textWriter = new System.IO.StreamWriter(schemaPath);
            writer = new Newtonsoft.Json.JsonTextWriter(textWriter);
            schema.WriteTo(writer);
            textWriter.Flush();
            textWriter.Dispose();

            schema = schemaGenerator.Generate(typeof(DTO.AcquisitionFile));
            schemaPath = @"..\..\..\..\..\..\docs\json\" + nameof(DTO.AcquisitionFileResult) + ".json";

            textWriter = new System.IO.StreamWriter(schemaPath);
            writer = new Newtonsoft.Json.JsonTextWriter(textWriter);
            schema.WriteTo(writer);
            textWriter.Flush();
            textWriter.Dispose();

            schema = schemaGenerator.Generate(typeof(DTO.AcquisitionFileDiff));
            schemaPath = @"..\..\..\..\..\..\docs\json\" + nameof(DTO.AcquisitionFileDiff) + ".json";

            textWriter = new System.IO.StreamWriter(schemaPath);
            writer = new Newtonsoft.Json.JsonTextWriter(textWriter);
            schema.WriteTo(writer);
            textWriter.Flush();
            textWriter.Dispose();

            Console.Read();

        }
    }
}
