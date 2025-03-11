using System;

namespace DWIS.API.DTO
{
    /// <summary>
    /// Specifications of each individual variable
    /// </summary>
    public class InjectedVariable : IEquatable<InjectedVariable>
    {
        /// <summary>
        /// The namespace where the variable lives in the source server. 
        /// Can be used when the namespace index is not know in advanced. 
        /// </summary>
        public string NativeAddressSpaceNameSpace{ get; set; }
        /// <summary>
        /// The NodeID of the variable in the source server. 
        /// Convention is 'namespace-index : id-type : id'
        /// namespace-index: if negative or not ushort, the namespace itself will be used instead
        /// id-type: 0 = int, 1 = string
        /// </summary>
        public string NativeAddressSpaceNodeID { get; set; }
        /// <summary>
        /// Used to construct the nodeID in the DDHub server. 
        /// Convention for Prediktor is:
        /// '2:1:V|{provider-name}.{InjectedName}'
        /// </summary>
        public string InjectedName { get; set; }
        /// <summary>
        /// Used to set-up subscription. 
        /// </summary>
        public double SamplingIntervalInMS { get; set; }

        public int Rank { get; set; } = 0;
        public int[] Dimensions { get; set; } = null;
        public string DataType { get; set; }

        public override bool Equals(object obj) => Equals(obj as InjectedVariable);
        public override int GetHashCode() => (NativeAddressSpaceNameSpace, NativeAddressSpaceNodeID, InjectedName, SamplingIntervalInMS).GetHashCode();


        public override string ToString()
        {
            return InjectedName + " [" + NativeAddressSpaceNameSpace + "/" + NativeAddressSpaceNodeID + "]";
        }


        public bool Equals(InjectedVariable other)
        {
            if (other is null)
                return false;

            return NativeAddressSpaceNameSpace == other.NativeAddressSpaceNameSpace 
                && NativeAddressSpaceNodeID == other.NativeAddressSpaceNodeID 
                && InjectedName == other.InjectedName 
                && SamplingIntervalInMS == other.SamplingIntervalInMS;
        }
    }

    public abstract class InjectedVariableIdentifier
    { }

    public class NodeIDIdentifier : InjectedVariableIdentifier, IEquatable<NodeIDIdentifier>
    {
        /// <summary>
        /// The namespace where the variable lives in the source server. 
        /// Can be used when the namespace index is not know in advanced. 
        /// </summary>
        public string NativeAddressSpaceNameSpace { get; set; }
        /// <summary>
        /// The NodeID of the variable in the source server. 
        /// Convention is 'namespace-index : id-type : id'
        /// namespace-index: if negative or not ushort, the namespace itself will be used instead
        /// id-type: 0 = int, 1 = string
        /// </summary>
        public string NativeAddressSpaceNodeID { get; set; }

        public override bool Equals(object obj) => Equals(obj as InjectedVariable);
        public override int GetHashCode() => (NativeAddressSpaceNameSpace, NativeAddressSpaceNodeID).GetHashCode();

        public bool Equals(NodeIDIdentifier other)
        {
            if (other is null)
                return false;

            return NativeAddressSpaceNameSpace == other.NativeAddressSpaceNameSpace 
                && NativeAddressSpaceNodeID == other.NativeAddressSpaceNodeID;
        }
    }

    public class BrowsePathIdentifier : InjectedVariableIdentifier
    {
        /// <summary>
        /// The namespace where the variable lives in the source server. 
        /// Can be used when the namespace index is not know in advanced. 
        /// </summary>
        public string StartNodeNativeAddressSpaceNameSpace { get; set; }
        /// <summary>
        /// The NodeID of the variable in the source server. 
        /// Convention is 'namespace-index : id-type : id'
        /// namespace-index: if negative or not ushort, the namespace itself will be used instead
        /// id-type: 0 = int, 1 = string
        /// </summary>
        public string StartNodeNativeAddressSpaceNodeID { get; set; }

        public RelativePathElement[] RelativePath { get; set; }
    }

   

    public class RelativePathElement
    {
        public string ReferenceNameSpace { get; set; }
        public string ReferenceID { get; set; }
        public bool IsInverse { get; set; }
        public bool IncludeSubTypes { get; set; }
        public string TargetName { get; set; }
    }
}
