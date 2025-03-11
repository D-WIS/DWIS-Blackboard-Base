using System;

namespace DWIS.API.DTO
{
    public class NodeIdentifier : IEquatable<NodeIdentifier>
    {
        public string NameSpace { get; set; }
        public string ID { get; set; }
               

        public override bool Equals(object obj) => Equals(obj as NodeIdentifier);
        public override int GetHashCode() => (NameSpace, ID).GetHashCode();

        public bool Equals(NodeIdentifier other)
        {
            if (other is null)
                return false;

            return NameSpace == other.NameSpace
                && ID == other.ID;
        }

        public override string ToString()
        {
            return NameSpace +  ID;
        }        
    }
}
