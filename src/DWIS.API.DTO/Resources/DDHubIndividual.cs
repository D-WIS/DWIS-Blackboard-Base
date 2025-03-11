using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWIS.API.DTO
{
    public class DDHubIndividual
    {
        public NodeIdentifier NodeIdentifier { get; set; }
        public string TypeDictionaryURI { get; set; }


        public override string ToString()
        {
            return NodeIdentifier.ToString() + " [" + TypeDictionaryURI + "]";
        }
    }

    public class DDHubSentence
    {
        public DDHubIndividual Subject { get; set; }
        public DDHubIndividual Object { get; set; }
        public string Verb { get; set; }
    }

    public class ResourceFile
    {
        public IList<DDHubIndividual> DDHubIndividuals { get; set; } = new List<DDHubIndividual>();
        public IList<DDHubSentence> DDHubSentences { get; set; } = new List<DDHubSentence>(); 
        
        public static ResourceFile FromJsonString(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResourceFile>(json);
        }
        public static string ToJsonString(ResourceFile file)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(file, Newtonsoft.Json.Formatting.Indented);
        }
    }
}
