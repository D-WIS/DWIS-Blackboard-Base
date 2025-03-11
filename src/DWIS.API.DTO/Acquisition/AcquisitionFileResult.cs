using System.Collections.Generic;

namespace DWIS.API.DTO
{
    public class AcquisitionFileResult
    {
        public IEnumerable<AcquisitionItemResolution> Resolutions { get; set; } = new List<AcquisitionItemResolution>(); 

        public static string ToJsonString(AcquisitionFileResult file)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(file, Newtonsoft.Json.Formatting.Indented);
        }
        public static AcquisitionFileResult FromJsonString(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AcquisitionFileResult>(json);
        }


    }

}
