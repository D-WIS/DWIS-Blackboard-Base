using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Schema.Generation;
using System.Linq;
using DWIS.Vocabulary.Development;

namespace DWIS.API.DTO
{
    /// <summary>
    /// A manifest file contains all the information required to publish semantical information to the main server. 
    /// </summary>
    public class ManifestFile
    {
        #region Schema

        /// <summary>
        /// The name of the manifest, used for further reference. 
        /// </summary>
        public string ManifestName { get; set; }

        /// <summary>
        /// Generic information about the publisher (provider) of the manifest. 
        /// </summary>
        public InjectionProvider Provider { get; set; }

        /// <summary>
        /// Generic information relevant to the injection of this specific manifest. 
        /// </summary>
        public InjectionInformation InjectionInformation { get; set; }

        /// <summary>
        /// List of variables that will be aggregated by the main server. This server will instanciate a client, that will subscribe to the defined variable in the address space defined in the manifest. The main server will then replicate the values (and the main attributes) locally. 
        /// </summary>
        public IList<InjectedVariable> InjectedVariables { get; set; }

        /// <summary>
        /// List of semantic nodes, used for the semantic description of the difference signals. 
        /// </summary>
        public IList<InjectedNode> InjectedNodes { get; set; }

        /// <summary>
        /// List of semantic sentences (subject - verb - object) used for the semantic description of the different signals. 
        /// </summary>
        public IList<InjectedReference> InjectedReferences { get; set; }

        /// <summary>
        /// List of variables, that the provider will directly write to. On injection, a variable will be instanciated by the server. No aggregation mechanism will be triggered. 
        /// </summary>
        public IList<ProvidedVariable> ProvidedVariables { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DWIS.Vocabulary.Development.Vocabulary InjectedVocabulary { get; set; }

        #endregion

        #region Edition helper methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataType"></param>
        /// <param name="rank"></param>
        /// <param name="dimensions"></param>
        /// <returns></returns>
        public bool AddProvidedVariable(string id, string dataType, int rank, int[] dimensions)
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(dataType) && rank >= 0)
            {
                if (ProvidedVariables == null) { ProvidedVariables = new List<ProvidedVariable>(); }
                var pv = new ProvidedVariable() { VariableID = id, DataType = dataType, Rank = rank };
                if (dimensions != null && dimensions.Length == rank)
                {
                    pv.Dimensions = dimensions;
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="typeID"></param>
        /// <param name="fieldData"></param>
        /// <returns></returns>
        public bool AddNode(string id, string typeID, params (string fieldName, string fieldValue)[] fieldData)
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(typeID))
            {
                if (InjectedNodes == null) { InjectedNodes = new List<InjectedNode>(); }
                InjectedNode iNode = new InjectedNode() { UniqueName = id, BrowseName = id, DisplayName = id, TypeDictionaryURI = typeID };
                InjectedNodes.Add(iNode);
                if (fieldData != null && fieldData.Length > 0)
                {
                    iNode.Fields = fieldData.Select(fd => new Field() { FieldName = fd.fieldName, FieldValue = fd.fieldValue }).ToList();
                }
                return true;
            }
            else return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subjectNamespace"></param>
        /// <param name="subjectID"></param>
        /// <param name="verb"></param>
        /// <param name="objectNamespace"></param>
        /// <param name="objectID"></param>
        /// <returns></returns>
        public bool AddReference(string subjectNamespace, string subjectID, string verb, string objectNamespace, string objectID)
        {
            if (!string.IsNullOrEmpty(subjectNamespace) && !string.IsNullOrEmpty(subjectID) && !string.IsNullOrEmpty(verb) && !string.IsNullOrEmpty(objectNamespace) && !string.IsNullOrEmpty(objectID))
            {
                if (InjectedReferences == null) { InjectedReferences = new List<InjectedReference>(); }
                NodeIdentifier refSubject = new NodeIdentifier() { ID = subjectID, NameSpace = subjectNamespace };
                NodeIdentifier refObject = new NodeIdentifier() { ID = objectID, NameSpace = objectNamespace };
                InjectedReference ir = new InjectedReference() { Subject = refSubject, VerbURI = verb, Object = refObject };
                InjectedReferences.Add(ir);
                return true;
            }
            else return false;
        }
        #endregion

        #region Static methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static ManifestFile FromJsonString(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ManifestFile>(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string ToJsonString(ManifestFile file)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(file, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        public void Absorb(ManifestFile other)
        {
            InjectedVariables = InjectedVariables.Union(other.InjectedVariables).ToList();
            InjectedNodes = InjectedNodes.Union(other.InjectedNodes).ToList();
            InjectedReferences = InjectedReferences.Union(other.InjectedReferences).ToList();
            ProvidedVariables = ProvidedVariables.Union(other.ProvidedVariables).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manifestFile"></param>
        /// <param name="obfuscateSourceVariables"></param>
        /// <param name="obfuscateInjectedVariables"></param>
        /// <param name="obfuscateInjectedNodes"></param>
        public static void ObfuscateManifest(ManifestFile manifestFile, bool obfuscateSourceVariables = true, bool obfuscateInjectedVariables = true, bool obfuscateInjectedNodes = true)
        {
            if (obfuscateInjectedNodes || obfuscateInjectedVariables || obfuscateSourceVariables)
            {
                System.Random random = new System.Random();

                if (obfuscateSourceVariables)
                {
                    foreach (var v in manifestFile.InjectedVariables)
                    {
                        string newTag = random.Next().ToString();
                        v.NativeAddressSpaceNodeID = newTag;
                    }
                }

                if (obfuscateInjectedVariables)
                {
                    foreach (var v in manifestFile.InjectedVariables)
                    {
                        string originalName = v.InjectedName;
                        string newTag = random.Next().ToString();
                        v.InjectedName = newTag;

                        var references = manifestFile.InjectedReferences.Where(i => i.Subject.NameSpace == manifestFile.InjectionInformation.InjectedVariablesNamespaceAlias && i.Subject.ID == originalName);
                        foreach (var r in references)
                        {
                            r.Subject.ID = newTag;
                        }
                        references = manifestFile.InjectedReferences.Where(i => i.Object.NameSpace == manifestFile.InjectionInformation.InjectedVariablesNamespaceAlias && i.Object.ID == originalName);
                        foreach (var r in references)
                        {
                            r.Object.ID = newTag;
                        }
                    }
                }

                if (obfuscateInjectedNodes)
                {
                    foreach (var v in manifestFile.InjectedNodes)
                    {
                        string originalName = v.UniqueName;
                        string newTag = random.Next().ToString();
                        v.UniqueName = newTag;
                        v.DisplayName = newTag;
                        v.BrowseName = newTag;

                        var references = manifestFile.InjectedReferences.Where(i => i.Subject.NameSpace == manifestFile.InjectionInformation.InjectedNodesNamespaceAlias && i.Subject.ID == originalName);
                        foreach (var r in references)
                        {
                            r.Subject.ID = newTag;
                        }
                        references = manifestFile.InjectedReferences.Where(i => i.Object.NameSpace == manifestFile.InjectionInformation.InjectedNodesNamespaceAlias && i.Object.ID == originalName);
                        foreach (var r in references)
                        {
                            r.Object.ID = newTag;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="providerName"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public static ManifestFile FromDWISInstance(DWIS.Vocabulary.Development.DWISInstance instance, string providerName, string companyName)
        {
            DWIS.API.DTO.ManifestFile manifestFile = new API.DTO.ManifestFile();
            manifestFile.InjectionInformation = new API.DTO.InjectionInformation();
            manifestFile.Provider = new API.DTO.InjectionProvider() { Company = companyName, Name = providerName };
            manifestFile.InjectedVariables = new List<API.DTO.InjectedVariable>();
            manifestFile.InjectedReferences = new List<API.DTO.InjectedReference>();
            manifestFile.InjectedNodes = new List<API.DTO.InjectedNode>();
            manifestFile.InjectedVocabulary = instance.InstanceVocabulary;
            if (instance.Population != null)
            {
                foreach (var n in instance.Population)
                {
                    if (n.TypeName == DWIS.Vocabulary.Schemas.Nouns.DynamicDrillingSignal)
                    {
                        manifestFile.InjectedVariables.Add(new API.DTO.InjectedVariable() { InjectedName = n.Name });
                    }
                    else
                    {
                        manifestFile.InjectedNodes.Add(new API.DTO.InjectedNode() { UniqueName = n.Name, DisplayName = n.Name, TypeDictionaryURI = n.TypeName });
                        if (n.Attributes != null)
                        {
                            var ino = manifestFile.InjectedNodes.Last();
                            foreach (var att in n.Attributes)
                            {
                                if (ino.Fields == null)
                                {
                                    ino.Fields = new List<API.DTO.Field>();
                                }
                                ino.Fields.Add(new API.DTO.Field() { FieldName = att.AttributeName, FieldValue = att.AttributeValue });
                            }
                        }
                    }
                }
            }

            if (instance.Sentences != null)
            {
                foreach (var s in instance.Sentences)
                {
                    string subjectNameSpace = manifestFile.InjectedVariables.FirstOrDefault(v => v.InjectedName == s.Subject) == null ? manifestFile.InjectionInformation.InjectedNodesNamespaceAlias : manifestFile.InjectionInformation.InjectedVariablesNamespaceAlias;
                    string objectNameSpace = manifestFile.InjectedVariables.FirstOrDefault(v => v.InjectedName == s.Object) == null ? manifestFile.InjectionInformation.InjectedNodesNamespaceAlias : manifestFile.InjectionInformation.InjectedVariablesNamespaceAlias;
                    manifestFile.InjectedReferences.Add(new API.DTO.InjectedReference()
                    {
                        Subject = new API.DTO.NodeIdentifier() { NameSpace = subjectNameSpace, ID = s.Subject },
                        VerbURI = "http://ddhub.no/" + s.Verb,
                        Object = new API.DTO.NodeIdentifier() { NameSpace = objectNameSpace, ID = s.Object }
                    });
                }
            }

            if (instance.ClassAssertions != null)
            {
                foreach (var c in instance.ClassAssertions)
                {
                    manifestFile.InjectedReferences.Add(new API.DTO.InjectedReference()
                    {
                        Subject = new API.DTO.NodeIdentifier() { NameSpace = manifestFile.InjectionInformation.InjectedNodesNamespaceAlias, ID = c.Subject },
                        VerbURI = "http://ddhub.no/" + c.Verb,
                        Object = new API.DTO.NodeIdentifier() { NameSpace = "http://ddhub.no/", ID = "http://ddhub.no/" + c.Class }
                    });
                }
            }

            if (instance.ImplicitSentences != null)
            {
                foreach (var imp in instance.ImplicitSentences)
                {
                    manifestFile.InjectedReferences.Add(new API.DTO.InjectedReference()
                    {
                        Subject = new API.DTO.NodeIdentifier() { NameSpace = manifestFile.InjectionInformation.InjectedNodesNamespaceAlias, ID = imp.Subject },
                        VerbURI = "http://ddhub.no/" + imp.Verb,
                        Object = new API.DTO.NodeIdentifier() { NameSpace = "http://ddhub.no/", ID = "http://ddhub.no/" + imp.Object }
                    });
                }
            }
            return manifestFile;
        }

        #endregion
    }
}
