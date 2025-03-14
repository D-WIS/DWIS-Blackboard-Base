using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DWIS.API.DTO
{
    public class RegisteredQueriesDiff
    {
        public List<QueryResultsDiff> ResultDiffs { get; private set; } = new List<QueryResultsDiff>();
    }
    public class QueryResultsDiff
    { 
        public string QueryID { get;  set; }
        public string QueryResultID { get; set; }
        public List<QueryResultRow> Added { get; set; } = new List<QueryResultRow>();
        public List<QueryResultRow> Removed { get; set; } = new List<QueryResultRow>();

        public bool IsEmpty()
        { 
        return Added.Count == 0 && Removed.Count == 0;
        }


        private QueryResultsDiff() { }

        public QueryResultsDiff(string id)
        {
            QueryID = id;
        }

        public QueryResultsDiff(string queryID, QueryResult original, QueryResult updated)
        {
            QueryID =  queryID;
            if (original == null && updated != null)
            {
                Added.AddRange(updated.Results);
            }
            else if (original != null && updated != null)
            {
                Added.AddRange(updated.Results.Where(ur => !original.Results.Contains(ur)));
                Removed.AddRange(original.Results.Where(ur => !updated.Results.Contains(ur)));
            }
        }



        public QueryResultsDiff(int queryID, QueryResult original, QueryResult updated)
        {
            QueryID = "QueryClient"+ queryID;
            
            if (original == null && updated!=null)
            {
                Added.AddRange(updated.Results);
            }
            else if(original != null && updated != null)
            {
                Added.AddRange(updated.Results.Where(ur => !original.Results.Contains(ur)));
                Removed.AddRange(original.Results.Where(ur => !updated.Results.Contains(ur)));
            }
        }
        public static QueryResultsDiff FromJsonString(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<QueryResultsDiff>(json);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            else return null;
        }
        public static string ToJsonString(QueryResultsDiff queryResultsDiff)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(queryResultsDiff, Newtonsoft.Json.Formatting.Indented);
        }
        public void PrettyPrint()
        {
            Console.WriteLine($"Query diff for query {QueryID}");
            Console.WriteLine($"Added items ({Added.Count} elements):");
            foreach (var added in Added)
            {
                Console.WriteLine($"{added}");
            }
            Console.WriteLine($"Added items ({Removed.Count} elements):");
            foreach (var removed in Removed)
            {
                Console.WriteLine($"{removed}");
            }
        }
    }

    public class QueryResult : IEquatable<QueryResult> 
    {
        public List<string> VariablesHeader { get; set; } = new List<string>();
        public List<QueryResultRow> Results { get; set; } = new List<QueryResultRow>();

        /// <summary>
        /// Returns the number of rows (i.e. results) in the table
        /// </summary>
        public int Count => ((ICollection<QueryResultRow>)Results).Count;

        public bool IsReadOnly => ((ICollection<QueryResultRow>)Results).IsReadOnly;

        public QueryResultRow this[int index] { get => Results[index]; set => Results[index] = value; }

        public IEnumerable<NodeIdentifier> GetResultsForVariable(string variableName)
        {
            if (Results != null && Results.Any())
            {
                int idx = GetVariableColumnIndex(variableName);
                if (idx >= 0) { return Results.Select(row => row[idx]); }
                else return null;
            }
            else return null;
        }


        public int GetVariableColumnIndex(string variableName)
        {
            return VariablesHeader.FindIndex(v => v.ToLower() == variableName.ToLower());
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }      

        public bool Equals(QueryResult other)
        {
            if (other == null) return false;

            if(Results.Count != other.Results.Count) return false;

            if(Results.Except(other.Results).Any()) return false;

            return true;

            return this.Results.SequenceEqual(other.Results);   
        }

        public void RemoveDuplicates()
        {
            if (Results != null)
            {
                Results = Results.Distinct<QueryResultRow>().ToList();
            }
        }

        public override string ToString()
        {
            string result = "";
            if (VariablesHeader != null)
            {
                foreach (var h in VariablesHeader)
                {
                    result += h + "\t";
                }
            }
            if (Results != null)
            {
                foreach (var r in Results)
                {
                    result += "\r\n" + r.ToString();
                }
            }
            return result;  
        }

        public string[,] ToStringArray()
        {
            string[,] result = new string[VariablesHeader.Count, Results.Count];
            for (int i = 0; i < Results.Count; i++)
            {
                for (int j = 0; j < VariablesHeader.Count; j++)
                {
                    result[j, i] = Results[i].Items[j].ToString();
                }
            }
            return result;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static QueryResult FromJsonString(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<QueryResult>(json);
        }
        public static string ToJsonString(QueryResult queryResults)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(queryResults, Newtonsoft.Json.Formatting.Indented);
        }

        public int IndexOf(QueryResultRow item)
        {
            return ((IList<QueryResultRow>)Results).IndexOf(item);
        }

        public void Insert(int index, QueryResultRow item)
        {
            ((IList<QueryResultRow>)Results).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<QueryResultRow>)Results).RemoveAt(index);
        }

        public void Add(QueryResultRow item)
        {
            ((ICollection<QueryResultRow>)Results).Add(item);
        }

        public void Clear()
        {
            ((ICollection<QueryResultRow>)Results).Clear();
        }

        public bool Contains(QueryResultRow item)
        {
            return ((ICollection<QueryResultRow>)Results).Contains(item);
        }

        public void CopyTo(QueryResultRow[] array, int arrayIndex)
        {
            ((ICollection<QueryResultRow>)Results).CopyTo(array, arrayIndex);
        }

        public bool Remove(QueryResultRow item)
        {
            return ((ICollection<QueryResultRow>)Results).Remove(item);
        }

        public IEnumerator<QueryResultRow> GetEnumerator()
        {
            return ((IEnumerable<QueryResultRow>)Results).GetEnumerator();
        }
    }


    public class QueryResultRow : IEquatable<QueryResultRow>
    {
        public List<NodeIdentifier> Items { get; set; } = new List<NodeIdentifier>();

        public int Count => ((ICollection<NodeIdentifier>)Items).Count;

        public bool IsReadOnly => ((ICollection<NodeIdentifier>)Items).IsReadOnly;

        public NodeIdentifier this[int index] { get => ((IList<NodeIdentifier>)Items)[index]; set => ((IList<NodeIdentifier>)Items)[index] = value; }

        public bool Equals(QueryResultRow other)
        {
            if (other == null || Items.Count != other.Items.Count) return false;

            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] == null && other.Items[i]!= null) return false;
                if (other.Items[i] == null) return false;
                if (!Items[i].Equals(other.Items[i])) return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is QueryResultRow)) return false;
            return Equals(obj as QueryResultRow);
        }

        public override string ToString()
        {
            string res = "";
            if (Items != null)
            {
                foreach (var h in Items)
                {
                    res += h + "\t";
                }
            }
            return res;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool Contains(string resourceURI)
        {
            return Items.FirstOrDefault(r => r.ToString() == resourceURI) != null;
        }

        public int IndexOf(NodeIdentifier item)
        {
            return ((IList<NodeIdentifier>)Items).IndexOf(item);
        }

        public void Insert(int index, NodeIdentifier item)
        {
            ((IList<NodeIdentifier>)Items).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<NodeIdentifier>)Items).RemoveAt(index);
        }

        public void Add(NodeIdentifier item)
        {
            ((ICollection<NodeIdentifier>)Items).Add(item);
        }

        public void Clear()
        {
            ((ICollection<NodeIdentifier>)Items).Clear();
        }

        public bool Contains(NodeIdentifier item)
        {
            return ((ICollection<NodeIdentifier>)Items).Contains(item);
        }

        public void CopyTo(NodeIdentifier[] array, int arrayIndex)
        {
            ((ICollection<NodeIdentifier>)Items).CopyTo(array, arrayIndex);
        }

        public bool Remove(NodeIdentifier item)
        {
            return ((ICollection<NodeIdentifier>)Items).Remove(item);
        }

        public IEnumerator<NodeIdentifier> GetEnumerator()
        {
            return ((IEnumerable<NodeIdentifier>)Items).GetEnumerator();
        }       
    }
}
