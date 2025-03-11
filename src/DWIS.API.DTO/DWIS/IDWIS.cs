using System;
using System.Collections.Generic;
using System.Text;
using static DWIS.API.DTO.Delegates;

namespace DWIS.API.DTO
{
    public interface IDWIS
    {
        //List<(string subject, string verb, string obj)> GetSentences();
        //IEnumerable<(string nameSpace, string id)> GetResources();
        string VariablesNamespace { get;  }
        //Put
        ManifestInjectionResult Inject(ManifestFile manifestFile);
        //Post
        bool Update(ManifestFile manifestFile);
        //Delete
        bool Delete(ManifestFile manifestFile);
        //Get
        AcquisitionFileResult Resolve(AcquisitionFile acquisitionFile);
        ResourceFile GetResources();

    }
    public interface IExtendedDWIS : IDWIS
    {
        QueryResult GetQueryResult(string sparql);
    }

    public interface IReactiveDWIS : IDWIS
    {
        string RegisterAcquisitionFile(AcquisitionFile acquisitionFile);
    }
    public interface IReactiveDWISClient : IDWIS
    {
        string RegisterAcquisitionFile(AcquisitionFile acquisitionFile, AcquisitionResultUpdateCallback callback);
    }
    public interface IReactiveExtendedDWIS : IExtendedDWIS, IReactiveDWIS
    {
        (string jsonQueryDiff, string nodeID) RegisterQuery(string sparql);
    }
    public interface IReactiveExtendedDWISClient : IExtendedDWIS, IReactiveDWISClient
    {
        (string jsonQueryDiff, string nodeID) RegisterQuery(string sparql, QueryResultUpdateCallBack callBack);
    }

    public static class Delegates
    {
        public delegate void QueryResultUpdateCallBack(QueryResultsDiff resultsDiff);
        public delegate void AcquisitionResultUpdateCallback(AcquisitionDiff acquisitionDiff);
    }
}
