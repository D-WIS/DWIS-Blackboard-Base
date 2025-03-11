using System.Collections.Generic;

namespace DWIS.API.DTO
{
    public class AcquisitionCriteriaResult
    {
        public int Index { get; set; }
        public IEnumerable<AcquisitionCriteriaResultItem> CriteriaResults { get; set; } = new List<AcquisitionCriteriaResultItem>();
    }
}
