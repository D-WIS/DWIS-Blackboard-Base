using System.Collections.Generic;

namespace DWIS.API.DTO
{
    public class AcquisitionItem
    {
        public string Name { get; set; }
        public IList<AcquisitionCriteria> Criterias { get; set; } = new List<AcquisitionCriteria>(); 
    }

}
