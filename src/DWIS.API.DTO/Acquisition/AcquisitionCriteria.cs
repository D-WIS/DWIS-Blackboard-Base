using System.Collections.Generic;

namespace DWIS.API.DTO
{
    public class AcquisitionCriteria
    {
        public int CriteriaIndex { get; set; }
        public IList<string> Classes { get; set; }
        public string SPARQL { get; set; }
    }

}
