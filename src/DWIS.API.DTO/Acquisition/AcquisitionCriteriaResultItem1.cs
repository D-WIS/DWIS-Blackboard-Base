namespace DWIS.API.DTO
{
    public class AcquisitionCriteriaResultItem
    {
        public DWISNodeID DataPointID { get; set; }
        public DWISNodeID SignalID { get; set; }
        public bool SignalDefined { get; set; }
    }
}
