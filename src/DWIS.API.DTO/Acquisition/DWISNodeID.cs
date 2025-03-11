using System;

namespace DWIS.API.DTO
{
    public class DWISNodeID : IEquatable<DWISNodeID>
    {
        public ushort NameSpaceIndex { get; set; }
        public string ID { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as DWISNodeID);
        }
        public override int GetHashCode()
        {
            return (NameSpaceIndex, ID).GetHashCode();
        }
        public bool Equals(DWISNodeID other)
        {
            return  ((DWISNodeID)other).ID == ID && ((DWISNodeID)other).NameSpaceIndex == NameSpaceIndex;
        }
        public override string ToString()
        {
            return "ns=" + NameSpaceIndex + ";s=" + ID;
        }
    }
}
