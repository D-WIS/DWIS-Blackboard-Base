using System.Collections.Generic;
namespace DWIS.API.DTO
{
    public class ManifestInjectionResult
    {
        public bool Success { get; set; }
        public string InjectedVariableNamespace { get; set; }
        public string InjectedNodesNamespace { get; set; }
        public string ProvidedVariablesNamespace { get; set; }
        public IList<InjectionMapping> InjectedVariables { get; set; }
        public IList<InjectionMapping> InjectedNodes { get; set; }
        public IList<InjectionMapping> ProvidedVariables { get; set; }

        public static ManifestInjectionResult FromJsonString(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ManifestInjectionResult>(json);
        }
        public static string ToJsonString(ManifestInjectionResult file)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(file, Newtonsoft.Json.Formatting.Indented);
        }

        public static ManifestInjectionResult ERROR { get; private set; } = new ManifestInjectionResult() { Success = false };
    }
}
