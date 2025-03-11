using System;
using System.Collections.Generic;
using System.Linq;

namespace DWIS.API.DTO
{
    public class AcquisitionCriteriaResultDiff
    {
        public int Index { get; set; }

        public IEnumerable<AcquisitionCriteriaResultItem> Added { get; set; } = new List<AcquisitionCriteriaResultItem>(); 
        public IEnumerable<AcquisitionCriteriaResultItem> Removed { get; set; } = new List<AcquisitionCriteriaResultItem>();

        public bool IsEmpty()
        {
            return Added.Count() == 0 && Removed.Count() == 0;
        }

        public AcquisitionCriteriaResultDiff(AcquisitionCriteriaResult previous, AcquisitionCriteriaResult latest)
        {
            Added = new List<AcquisitionCriteriaResultItem>();
            Removed = new List<AcquisitionCriteriaResultItem>();

            if (previous == null)
            {
                Added = Added.Concat(latest.CriteriaResults);
            }
            else
            {
                Removed = previous.CriteriaResults.Except(latest.CriteriaResults);
                Added = latest.CriteriaResults.Except(previous.CriteriaResults);
            }
        }

        public static void PrettyPrint(AcquisitionCriteriaResultDiff acquisitionCriteriaResultDiff)
        {
            Console.WriteLine($"Criteria index: {acquisitionCriteriaResultDiff.Index}");
            Console.WriteLine("Removed items: ");
            foreach (var it in acquisitionCriteriaResultDiff.Removed)
            {
                Console.WriteLine($"Namespace: {it.DataPointID.NameSpaceIndex}, ID: {it.DataPointID.ID}");
            }

            Console.WriteLine("Added items: ");
            foreach (var it in acquisitionCriteriaResultDiff.Added)
            {
                Console.WriteLine($"Namespace: {it.DataPointID.NameSpaceIndex}, ID: {it.DataPointID.ID}");
            }
        }
    }

}
