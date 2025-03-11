using System.Collections.Generic;
using System;
using System.Linq;
namespace DWIS.API.DTO
{
    public class InjectedNode : IEquatable<InjectedNode>
    {
        public string TypeDictionaryURI { get; set; }
        public string BrowseName { get; set; }
        public string UniqueName { get; set; }

        public string DisplayName { get; set; }
        public IList<Field> Fields { get; set; }

        public override bool Equals(object obj) => Equals(obj as InjectedNode);
        public override int GetHashCode() => (TypeDictionaryURI, UniqueName).GetHashCode();

        public bool Equals(InjectedNode other)
        {
            if (other is null)
                return false;

            if (TypeDictionaryURI == other.TypeDictionaryURI && BrowseName == other.BrowseName && UniqueName == other.UniqueName && DisplayName == other.DisplayName)
            {
                if (Fields == null && other.Fields == null)
                {
                    return true;
                }
                else
                {
                    return  Fields.Count() == other.Fields.Count() &&  Fields.Intersect(other.Fields).Count() == Fields.Count();
                }
            }
            else return false;
        }

        public override string ToString()
        {
            return UniqueName + " [" + TypeDictionaryURI + "]";
        }
    }

}
