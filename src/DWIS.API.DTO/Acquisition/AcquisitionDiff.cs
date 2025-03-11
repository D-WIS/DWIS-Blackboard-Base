using System;
using System.Collections.Generic;
using System.Linq;

namespace DWIS.API.DTO
{
    public class AcquisitionDiff
    {
        public IEnumerable<AcquisitionFileDiff> AcquisitionFileDiffs { get; set; } = new List<AcquisitionFileDiff>();

        public static AcquisitionDiff FromJsonString(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AcquisitionDiff>(json);
        }
        public static string ToJsonString(AcquisitionDiff file)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(file, Newtonsoft.Json.Formatting.Indented);
        }

        public static void PrettyPrint(AcquisitionDiff acquisitionDiff)
        {
            if (acquisitionDiff != null)
            {
                Console.WriteLine($"Acquisition diff: {acquisitionDiff.AcquisitionFileDiffs.Count()} files affected.");
                foreach (var f in acquisitionDiff.AcquisitionFileDiffs)
                {
                    AcquisitionFileDiff.PrettyPrint(f);
                }
            }
            else
            {
                Console.WriteLine("AcquisitionDiff file null.");
            }
        }

    }

}
