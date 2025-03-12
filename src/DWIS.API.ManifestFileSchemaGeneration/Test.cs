using System;
using System.Collections.Generic;
using System.Text;

namespace DWIS.API.ManifestFileSchemaGeneration
{

    public class Rootobject
    {
        public Provider Provider { get; set; }
        public Injectioninformation InjectionInformation { get; set; }
        public Injectedvariable[] InjectedVariables { get; set; }
        public Injectednode[] InjectedNodes { get; set; }
        public Injectedreference[] InjectedReferences { get; set; }
    }

    public class Provider
    {
        public string Name { get; set; }
        public string Company { get; set; }
    }

    public class Injectioninformation
    {
        public string ServerName { get; set; }
        public string EndPointURL { get; set; }
        public float PublishingIntervalInMS { get; set; }
        public string InjectedVariablesNamespace { get; set; }
        public string InjectedNodesNamespace { get; set; }
    }

    public class Injectedvariable
    {
        public string NativeAddressSpaceNodeID { get; set; }
        public string InjectedBrowseName { get; set; }
        public float SamplingIntervalInMS { get; set; }
    }

    public class Injectednode
    {
        public string TypeURI { get; set; }
        public string BrowseName { get; set; }
        public object Fields { get; set; }
    }

    public class Injectedreference
    {
        public Subject Subject { get; set; }
        public string VerbURI { get; set; }
        public Object Object { get; set; }
    }

    public class Subject
    {
        public int NamespaceIndex { get; set; }
        public string BrowseName { get; set; }
    }

    public class Object
    {
        public string BrowseName { get; set; }
    }



}
