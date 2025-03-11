using System;
using System.Collections.Generic;
using System.Linq;

namespace DWIS.API.DTO
{
    public class AcquisitionFileDiff
    {
        public int AcquisitionFileID { get; set; }
        public IEnumerable<AcquisitionItemDiff> ItemDiffs { get; set; } = new List<AcquisitionItemDiff>();

        public static void PrettyPrint(AcquisitionFileDiff acquisitionFileDiff)
        {
            Console.WriteLine($"Acquisition file ID {acquisitionFileDiff.AcquisitionFileID}: {acquisitionFileDiff.ItemDiffs.Count()} item(s) affected");
            foreach (var itemDiff in acquisitionFileDiff.ItemDiffs)
            {
                AcquisitionItemDiff.PrettyPrint(itemDiff);
            }
        }


        public static AcquisitionFileDiff FromJsonString(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AcquisitionFileDiff>(json);
        }
        public static string ToJsonString(AcquisitionFileDiff file)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(file, Newtonsoft.Json.Formatting.Indented);
        }

    }

}
