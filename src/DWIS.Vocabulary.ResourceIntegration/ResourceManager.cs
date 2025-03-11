using DWIS.Vocabulary.Development;
using DWIS.API.DTO;

namespace DWIS.Vocabulary.ResourceIntegration
{
    public class ResourceManager
    {
        public static string DDHubURIPrefix = "http://ddhub.no/";
        public static string NameSpaceSeparator = "/";
        public static string ProvidedVariablesSuffix = "Variables";
        public static string GetAggregatedVariableID(string providerName, string injectedVariableName)
        {
            return $"{providerName}.{injectedVariableName}";
        }

        public static string GetProvidedVariableID(string providerName, string providedVariableName)
        {
            return $"{providerName}.{providedVariableName}";
        }

        public static string GetProvidedVariablesNamespace(string providerName) 
        {
            return DDHubURIPrefix + providerName + NameSpaceSeparator + ProvidedVariablesSuffix + NameSpaceSeparator;
        }

        public static string GetNodesNamespace(string providerName)
        {
            return  DDHubURIPrefix + providerName + NameSpaceSeparator;
        }
        public static string GetInjectedVocabularyNamespace(string providerName)
        {
        return DDHubURIPrefix + providerName + NameSpaceSeparator + "Semantic" + NameSpaceSeparator;
        }

        public static string GetRDFSemanticResource(string providerName, string semanticItemName) 
        {
            return GetInjectedVocabularyNamespace(providerName) + semanticItemName;
        }

        public static string GetRDFVariableResource(string variableNamespace, string providerName, string injectedVariableName)
        {
            return variableNamespace + NameSpaceSeparator + GetAggregatedVariableID(providerName, injectedVariableName);
        }

        public static string GetRDFProvidedVariableResource( string providerName, string injectedVariableName)
        {
            return GetProvidedVariablesNamespace(providerName) + GetProvidedVariableID(providerName, injectedVariableName);
        }

        public static string GetRDFNodeResource(string providerName, string injectedNodeName)
        { 
            return GetNodesNamespace(providerName) + injectedNodeName; 
        }

        public static string GetRDFPropertyResource(string propertyName)
        {
            return DDHubURIPrefix + propertyName;
        }

        public static bool GetNamespaceAndIdFromUri(string uri, out string ns, out string id)
        {
            if (!string.IsNullOrEmpty(uri))
            {
                int idx = uri.LastIndexOf("^^");
                if (idx >= 0)
                {
                    ns = uri.Substring(idx + 2);
                    id = uri.Substring(0, idx);
                    return true;
                }
                else
                {
                    idx = uri.LastIndexOf(NameSpaceSeparator);
                    if (idx >= 0 && idx < uri.Length - 1)
                    {
                        ns = uri.Substring(0, idx+1);
                        id = uri.Substring(idx + 1);
                        return true;
                    }
                }
            }
            ns = id = string.Empty;
            return false;
        }
        public static NodeIdentifier? GetNodeIdentifierFromUri(string uri)
        {
            if (GetNamespaceAndIdFromUri(uri, out string ns, out string id))
            {              
                    return new NodeIdentifier() { NameSpace = ns, ID = id };
                
            }
            else return null;
        }
        public static bool GetNodeIDentifierFromUri(string uri, out NodeIdentifier? nodeIdentifier)
        {
            nodeIdentifier = GetNodeIdentifierFromUri(uri);
            return nodeIdentifier != null;
        }

        public static string ToUri(NodeIdentifier nodeIdentifier)
        {
            if (nodeIdentifier != null)
            {
                if (nodeIdentifier.NameSpace.EndsWith(NameSpaceSeparator))
                {
                    return nodeIdentifier.NameSpace + nodeIdentifier.ID;
                }
                else return nodeIdentifier.NameSpace + NameSpaceSeparator + nodeIdentifier.ID;            
            }
            else return string.Empty;
        }


        public static DDHubIndividual? FromUri(string uri, string type)
        {
            if (GetNodeIDentifierFromUri(uri, out NodeIdentifier? nodeIdentifier))
            {
                return new DDHubIndividual() { NodeIdentifier = nodeIdentifier, TypeDictionaryURI = type };
            }
            else return null;
        }
    }
}