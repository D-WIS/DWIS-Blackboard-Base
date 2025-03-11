using System;
using System.Collections.Generic;
using System.Linq;

namespace DWIS.API.DTO
{
    public class AcquisitionItemDiff
    {
        public string Name { get; set; }
        public IEnumerable<AcquisitionCriteriaResultDiff> CriteriaDiffs { get; set; } = new List<AcquisitionCriteriaResultDiff>(); 
        public AcquisitionItemDiff(AcquisitionItem acquisitionItem, AcquisitionItemResolution previousResolution, AcquisitionItemResolution lastResolution)
        {
            Name = acquisitionItem.Name;

            CriteriaDiffs = acquisitionItem.Criterias.Select(
                c => new AcquisitionCriteriaResultDiff(
                    previousResolution != null ? previousResolution.ITemResults.FirstOrDefault(r => r.Index == c.CriteriaIndex) : null,
                    lastResolution.ITemResults.FirstOrDefault(r => r.Index == c.CriteriaIndex))
            {  Index = c.CriteriaIndex}).Where(d => !d.IsEmpty());
        }

        public bool IsEmpty()
        {
            return CriteriaDiffs.Count() == 0;
        }

        public static void PrettyPrint(AcquisitionItemDiff acquisitionItemDiff)
        {
            Console.WriteLine($"Item name: {acquisitionItemDiff.Name}");
            foreach (var item in acquisitionItemDiff.CriteriaDiffs)
            {
                AcquisitionCriteriaResultDiff.PrettyPrint(item);
            }
        }

    }

}
