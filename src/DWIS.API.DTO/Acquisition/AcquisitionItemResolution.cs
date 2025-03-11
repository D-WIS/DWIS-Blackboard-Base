using System.Collections.Generic;

namespace DWIS.API.DTO
{
    public class AcquisitionItemResolution
    {
        public string Name { get; set; }
        public IEnumerable<AcquisitionCriteriaResult> ITemResults { get; set; } = new List<AcquisitionCriteriaResult>(); 

    }

}
