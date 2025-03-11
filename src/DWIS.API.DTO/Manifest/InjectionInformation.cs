using System;

namespace DWIS.API.DTO
{
    /// <summary>
    /// Information required to set-up the "internal" client
    /// </summary>
    public class InjectionInformation : IEquatable<InjectionInformation>
    {
        public string ServerName { get; set; }
        public string EndPointURL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double PublishingIntervalInMS { get; set; }
        public string InjectedVariablesNamespaceAlias { get; set; } = "Variables";
        public string InjectedNodesNamespaceAlias { get; set; } = "Nodes";
        public string ProvidedVariablesNamespaceAlias { get; set; } = "ProvidedVariables";
        public string InjectedVocabularyNamespaceAlias { get; set; } = "Vocabulary";
        public override bool Equals(object obj) => Equals(obj as InjectionInformation);
        public override int GetHashCode() => (ServerName, EndPointURL, PublishingIntervalInMS, InjectedVariablesNamespaceAlias, InjectedNodesNamespaceAlias).GetHashCode();

        public bool Equals(InjectionInformation other)
        {
            if (other is null)
                return false;

            return ServerName == other.ServerName
                && EndPointURL == other.EndPointURL
                && PublishingIntervalInMS == other.PublishingIntervalInMS
                && InjectedNodesNamespaceAlias == other.InjectedNodesNamespaceAlias
                && InjectedVariablesNamespaceAlias == other.InjectedVariablesNamespaceAlias 
                && ProvidedVariablesNamespaceAlias == other.ProvidedVariablesNamespaceAlias;
        }
    }
}
