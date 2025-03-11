using System;
using System.Collections.Generic;
using System.Text;

namespace DWIS.OPCUA.Schemas
{
    public static partial class TypesIds
    {
        public static readonly string DDHubTypeDictionaryEntry = "DDHubTypeDictionaryEntryType";
        public static readonly string DDHubReferenceDictionaryEntry = "DDHubReferenceDictionaryEntryType";
        public static readonly string DDHubClassDictionaryEntry = "DDHubClassDictionaryEntryType";
        public static readonly string DDHubServerType = "DDHubServerType";

        public static readonly string AcquisitionEventType = "AcquisitionEventType";
        public static readonly string AcquisitionClientType = "AcquisitionClient";

    }

    public static partial class ReferencesIds
    {
         public static readonly string HasDDHubTypeDictionaryEntry = "HasDDHubTypeDictionaryEntryReferenceType";
         public static readonly string HasDDHubClassDictionaryEntry = "HasDDHubClassDictionaryEntryReferenceType";
         public static readonly string HasAssertedDDHubClassDictionaryEntry = "HasAssertedDDHubClassDictionaryEntryReferenceType";
         public static readonly string HasInferredDDHubClassDictionaryEntry = "HasInferredDDHubClassDictionaryEntryReferenceType";
         public static readonly string HasDDHubReferenceDictionaryEntry = "HasDDHubReferenceDictionaryEntryReferenceType";

         public static readonly string HasDDHubTypeDictionaryEntry_Inverse = "ContainsDDHubTypeReferenceType";
         public static readonly string HasDDHubClassDictionaryEntry_Inverse = "ContainsElementReferenceType";
         public static readonly string HasAssertedDDHubClassDictionaryEntry_Inverse = "ContainsAssertedElementReferenceType";
         public static readonly string HasInferredDDHubClassDictionaryEntry_Inverse = "ContainsInferredElementReferenceType";
         public static readonly string HasDDHubReferenceDictionaryEntry_Inverse = "ContainsDDHubReferenceReferenceType";


    }

    public static partial class DisplayNames
    {
        public static readonly string DDHubTypeDictionaryEntry = "DDHubTypeDictionaryEntry";
        public static readonly string DDHubReferenceDictionaryEntry = "DDHubReferenceDictionaryEntry";
        public static readonly string DDHubClassDictionaryEntry = "DDHubClassDictionaryEntry";

        public static readonly string HasDDHubTypeDictionaryEntry = "HasDDHubTypeDictionaryEntry";
        public static readonly string HasDDHubClassDictionaryEntry = "HasDDHubClassDictionaryEntry";
        public static readonly string HasAssertedDDHubClassDictionaryEntry = "HasAssertedDDHubClassDictionaryEntry";
        public static readonly string HasInferredDDHubClassDictionaryEntry = "HasInferredDDHubClassDictionaryEntry";
        public static readonly string HasDDHubReferenceDictionaryEntry = "HasDDHubReferenceDictionaryEntry";

        public static readonly string HasDDHubTypeDictionaryEntry_Inverse = "ContainsDDHubType";
        public static readonly string HasDDHubClassDictionaryEntry_Inverse = "ContainsElement";
        public static readonly string HasAssertedDDHubClassDictionaryEntry_Inverse = "ContainsAssertedElement";
        public static readonly string HasInferredDDHubClassDictionaryEntry_Inverse = "ContainsInferredElement";
        public static readonly string HasDDHubReferenceDictionaryEntry_Inverse = "ContainsDDHubReference";

        public static readonly string DDHubServerType = "DDHubServer";
        public static readonly string DDHubServerInstance = "DDHubServer";

        public static readonly string AcquisitionEventType = "AcquisitionEvent";
        public static readonly string AcquisitionClientType = "AcquisitionClient";


    }

    public static partial class MethodsIDs
    {
        public static string InjectManifestMethodName = "Inject";
        public static string DeleteManifestMethodName = "Delete";
        public static string UpdateManifestMethodName = "Update";
        public static string ResolveQueryMethodName = "ResolveQuery";
        public static string RegisterQueryMethodName = "RegisterQuery";
        public static string ResolveAcquisitionFileMethodName = "Resolve";
        public static string RegisterAcquisitionFileMethodName = "Register";
        public static string GetResourcesMethodName = "GetResources";
    }

}
