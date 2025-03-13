# DWIS-Blackboard-Base

## DWIS.API.DTO

The project contains the definitions and implementations of the main objects used to interact with a **DWIS Blackboard**. The interactions defined by the blackboard API are:
- *manifest injection* to push semantic information on the *blackboard*, and create variables for the live signals
- *query resolution* to send a SPARQL query to the *blackboard*
- *query registration* to register a SPARQL query on the **blackboard**, so that a notification will be issued each time a change in the query's results is detected. 

### Manifest injection

Semantic information is provided to the **blackboard** by sending a json serialized **ManifestFile**. The semantic information can consist in *nodes* or *variables*, the latter corresponding to real-time signals. There are two mechanisms available for the *variables*:
- *variable injection*: the real-time signal already exists in an (external) OPC-UA server. Upon injection, corresponding *variable nodes* are created on the **blackboard** and an OPC-UA client is launched that will subscribe to the external signals, and will replicate into its own address space.
- *variable provision*: upon injection of the manifest file, *variable nodes* are created on the **blackboard**. It is up to external actors to update the values of the *variables* directly on the **blackboard**. 

The semantic description of the data then consits in providing the following:
- a list of *semantic nodes*, used for the description of the signals
- a list of *semantic sentences*, in the form *subject - verb - object* to link *semantic nodes* and/or *variables* together. 

Here is an overview of what a manifest file can/should contain:

- `ManifestName` (*optional*, `string`): the name given to the manifest. It is only used for logging purposes
- `InjectionProvider` (*Mandatory*): contains information about the application that provides the semantic information. A dedicated *semantic node* representing the provider will be created upon manifest injection. 
  - `Name` (*Mandatory*, `string`): the name of the application. The **blackboard** uses the name to generate namespaces and variable id's to the different nodes that are injected. 
  - `Company` (*Optional*, `string`)
- `InjectionInformation` (*Mandatory*)
  - `InjectedVariablesNamespaceAlias` (*Mandatory*, `string`, default: `"Variables"`): a local alias for the *injected variables*, for references in the `InjectedReference` section. Not used when the *variable injection* feature is not used. 
  - `InjectedNodesNamespaceAlias` (*Mandatory*, `string`, default: `"Nodes"`): : a local alias for the *semantic nodes*, for references in the `InjectedReference` section. 
  - `ProvidedVariablesNamespaceAlias` (*Mandatory*, `string`, default: `"ProvidedVariables"`): a local alias for the *provided variables*, for references in the `InjectedReference` section. Not used when the *variable provision* feature is not used. 
  - `InjectedVocabularyNamespaceAlias` (*Mandatory*, `string`, default: `"Vocabulary"`): a local alias for the *injected vocabulary*, a feature not covered yet by the implementation. 
  - `ServerName` (*Optional*, `string`): for the *variable injection* scenario, the name of the external server containing the real-time signals.
  - `EndPointURL` (*Optional*, `string`): for the *variable injection* scenario, the url of the external server containing the real-time signals.
  - `PublishingIntervalInMS` (*Optional*, `double`): for the *variable injection* scenario, the expected refresh-rate of the variables in the external server. 
- `InjectedNodes` (*Optional*, `[InjectedNode]`): the list of *semantic nodes* used to describe the signals. Each item is of type `InjectedNode`:
  - `UniqueName` (*Mandatory*, `string`): the identifier for the *semantic node*
  - `BrowseName` (*Optional*, `string`): the OPC-UA browse name for the *semantic node*
  - `DisplayName` (*Optional*, `string`): the display name for the *semantic node*
  - `TypeDictionaryURI` (*Mandatory*, `string`): the base type of the *semantic node*. Additional classes can be specified in the `InjectedReferences` section. 
  - `Fields` (*Optional*): the list of attributes defined for the `semantic nodes`. Each item is of type `Field`:
    - `FieldName` (*Mandatory*, `string`): the name of the field. 
    - `FieldValue` (*Mandatory*, `string`): the string version of the field. 
- `InjectedVariables` (*Optional*, `[InjectedVariable]`): the list of *variables* in the *variable injection* scenario. Each item is of type `InjectedVariable`:
  - `NativeAddressSpaceNameSpace` (*Mandatory*, `string`): the namespace of the variable in the external OPC-UA server.
  - `NativeAddressSpaceNodeID` (*Mandatory*, `string`): the string id of the variable in the external OPC-UA server. 
  - `InjectedName` (*Mandatory*, `string`): the id of the variable, used to assign an id in the **blackboard** and for further references in the `InjectedReferences` section. 
  - `SamplingIntervalInMS` (*Optional*, `double`): the specific refresh rate of this variable. 
- `ProvidedVariables` (*Optional*, `[ProvidedVariable]`): the list of *variables* in the *variable provision* scenario. Each item is of type `ProvidedVariable`:
  - `VariableID` (*Mandatory*, `string`): the id of the variable, used to assign an id in the **blackboard** and for further references in the `InjectedReferences` section. 
  - `Rank` (*Optional*, `int`, default: `0`): the rank of the *variable*, corresponding of the number of indices required to access a single scalar value. For a scalar, the value is `0`, a one-dimensional array `1`. 
  - `Dimensions` (*Optional*, `[int]`, default: `null`): the shape of the variable. For a 1-dimensional array with *n* entries, the value is `[1]`. For a *n* x *m* matrix, the value is `[n, m]`.
  - `DataType` (*Mandatory*, `string`): the base type of the *variable*. For an array of floats, the value is *float*. The list of currently managed data types is: `bool`, `int`, `uint`, `string`, `long`, `ulong`, `double`, `float`, `short`, `ushort`. 
- `InjectedReferences` (*Optional*, `[InjectedReferences]`): the list of *semantic sentences* used to describe the signals. All those sentences are of the form *(subject-verb-object)*. Typically, the *subject* is a *semantic resource*, i.e. a *node* or *variable* defined in the manifest or already present on the **blackboard**. The *verb* has to come from the DWIS vocabulary. The *object* is typically also a *semantic resource*. The *object* can be a DWIS noun in the following cases: when specifying that the *subject* belongs to a specific class, in which case the verb has to be *BelongsToClass*, or when using *blank nodes*, a special scenario. Each item in the list is of type `InjectedReference`:
  - `Subject` (*Mandatory*, `NodeIdentifier`). A `NodeIdentifier` is described by:
    - `NameSpace` (*Mandatory*, `string`): the namespace of the *semantic resource*. If already present on the **blackboard**, it should be the namespace in the **blackboard** address space. If it corresponds to a *resource* defined in the current manifest, the aliases defined in the `InjectionInformation` section should be used. 
    - `ID` (*Mandatory*, `string`): the ID of the *semantic resource*. 
  - `Verb` (*Mandatory*, `string`): the verb of the *semantic sentence*. Should be present in the DWIS vocabulary. 
  - `Object` (*Mandatory*, `NodeIdentifier`).
- `InjectedVocabulary` (*Optional*): when injected a custom vocabulary. This feature is not implemented yet. 
