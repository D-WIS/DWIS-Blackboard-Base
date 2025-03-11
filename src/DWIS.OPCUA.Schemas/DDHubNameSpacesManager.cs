using System.Collections.Generic;

namespace DWIS.OPCUA.Schemas
{ 
    public class DDHubNameSpacesManager
    {
        public ushort DDHubDictionariesNameSpaceIndex { get; set; }

        public ushort DDHubTypesNameSpaceIndex { get; set; }

        public string InjectedNodesNamespace { get; set; }
        public string InjectedVariablesNamespace { get; set; }

        public string ProvidedVariablesNamespace { get; set; }
        public ushort ProvidedVariablesNamespaceIndex { get; set; }

        public ushort InjectedNodesNamespaceIndex { get; set; }
        public ushort InjectedVariablesNamespaceIndex { get; set; }

        public List<string> AdditionalNamespaces { get; set; }

    }
}
