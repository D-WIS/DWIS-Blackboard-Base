using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWIS.Vocabulary.Schemas;
using RDFSharp.Query;

namespace DWIS.SPARQL.Utils
{
    public class QueryBuilder
    {
        public static readonly string SIGNAL_VARIABLE = "?signal";
        public static readonly string DATAPOINT_VARIABLE = "?dataPoint";
        private static readonly string UNIT = "?unit";
        private static readonly string UNITADD = "?fromSIAdd";
        private static readonly string UNITMULT = "?fromSIMult";
        private static readonly string PROVIDER = "?provider";

        public static readonly string DDHUBPREFIX = "ddhub:";// "http://ddhub.no/";
        public static readonly string RDFTYPE = "rdf:type";//  "http://www.w3.org/1999/02/22-rdf-syntax-ns#type";
        public static readonly string RDFSSUBCLASSOF = "<http://www.w3.org/2000/01/rdf-schema#:subClassOf>";
        private static readonly string PATTERNTAB = "\t\t\t";

        private List<string> _selects = new();
        private List<string> _classes = new();
        private List<(string sub, string verb, string obj)> _patterns = new();

        private bool _signalAdded = false;
        private bool _dataPointAdded = false;
        private bool _unitAdded = false;
        private bool _unitAddAdded = false;
        private bool _unitMultAdded = false;
        private bool _providerAdded = false;

        public string? Query { get; private set; }


        public QueryBuilder Reset()
        {
            _signalAdded = false;
            _dataPointAdded = false;
            _unitAdded = false;
            _unitAddAdded = false;
            _unitMultAdded = false;
            _providerAdded = false;
            _patterns.Clear();
            _selects.Clear();
            _classes.Clear();
            return this;
        }

        public QueryBuilder SelectSignal()
        {
            if(!_selects.Contains(SIGNAL_VARIABLE))
            _selects.Add(SIGNAL_VARIABLE);
            return this;
        }

        public QueryBuilder SelectDataPoint()
        {
            if (!_selects.Contains(DATAPOINT_VARIABLE))
                _selects.Add(DATAPOINT_VARIABLE);
            return this;
        }

        public QueryBuilder SelectUnit()
        {
            if (!_selects.Contains(UNIT))
                _selects.Add(UNIT);
            return this;
        }

        public QueryBuilder SelectUnitConversion()
        {
            if (!_selects.Contains(UNITADD))
                _selects.Add(UNITADD);
            if (!_selects.Contains(UNITMULT))
                _selects.Add(UNITMULT);
            return this;
        }

        public QueryBuilder SelectProvider()
        {
            if (!_selects.Contains(PROVIDER))
                _selects.Add(PROVIDER);
            return this;
        }

        public QueryBuilder AddSelectedVariable(string variable)
        {
            if (!_selects.Contains(variable))
                _selects.Add(variable);
            return this;
        }

        public QueryBuilder AddClasses(params string[] classes)
        {
            _classes.AddRange(classes);
            return this;
        }

        /// <summary>
        /// Adds a (?dataPoint rdf:type ddhub:Measurement) triplet. 
        /// </summary>
        /// <param name="measurementClass"></param>
        /// <returns></returns>
        public QueryBuilder AddMeasuredClass(string measurementClass)
        {
            if (!_classes.Contains(Nouns.Measurement))
            {
                _classes.Add(Nouns.Measurement);
            }

            _classes.Add(measurementClass);            
            return this;
        }

        public QueryBuilder AddPattern(IEnumerable<(string sub, string verb, string obj)> pattern)
        {
            _patterns.AddRange(pattern);
            return this;
        }
        /// <summary>
        /// Will include the triplet in the SELECT clauses. 
        /// Note that unless the rdf:type is specified as a verb, the ddhub prefix will be automatically included in front of the verb. 
        /// </summary>
        /// <param name="sub"></param>
        /// <param name="verb"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public QueryBuilder AddPatternItem(string sub, string verb, string obj)
        {
            _patterns.Add((sub, verb, obj));
            return this;
        }

        /// <summary>
        /// Generates the query, and returns it. 
        /// </summary>
        /// <returns></returns>
        public string Build()
        {
            Query = BuildQuery();
            return Query;
        }

        private string BuildQuery()
        {
            _dataPointAdded = _providerAdded = _signalAdded = _unitAddAdded = _unitAdded = _unitMultAdded = false;


            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>");
            stringBuilder.AppendLine("PREFIX ddhub: <http://ddhub.no/>");

            GenerateSelectLine(stringBuilder);

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("WHERE {");

            foreach (var item in _selects)
            {
                if (item == SIGNAL_VARIABLE)
                {
                    AddSignal(stringBuilder);
                }
                else if (item == UNIT)
                {
                    AddUnit(stringBuilder);
                }
                else if (item == UNITADD)
                {
                    AddUnitAdd(stringBuilder);
                }
                else if (item == UNITMULT)
                {
                    AddUnitMult(stringBuilder);
                }
                else if (item == PROVIDER)
                {
                    AddProvider(stringBuilder);
                }
            }

            foreach (var className in _classes)
            {
                AddClass(stringBuilder, className);
            }

            foreach (var patternItem in _patterns)
            {
                AddPatternItem(stringBuilder, patternItem);
            }

            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        private void AddClass(StringBuilder stringBuilder, string className) 
        {
            stringBuilder.AppendLine($"{PATTERNTAB}{DATAPOINT_VARIABLE} {RDFTYPE} {DDHUBPREFIX}{className} .");
        }

        private void AddProvider(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"{PATTERNTAB}{DATAPOINT_VARIABLE} {DDHUBPREFIX}{Verbs.IsProvidedBy} {PROVIDER} .");
            _providerAdded = true;
        }

        private void AddUnit(StringBuilder stringBuilder)
        {
            if (!_signalAdded && !_selects.Contains(SIGNAL_VARIABLE)) { AddSignal(stringBuilder); }
            stringBuilder.AppendLine($"{PATTERNTAB}{SIGNAL_VARIABLE} {DDHUBPREFIX}{Verbs.HasUnitOfMeasure} {UNIT} .");
            _unitAdded = true;
        }

        private void AddUnitAdd(StringBuilder stringBuilder) 
        {
            if (!_unitAdded && !_selects.Contains(UNIT)) { AddUnit(stringBuilder); };
            stringBuilder.AppendLine($"{PATTERNTAB}{UNIT} {DDHUBPREFIX}{Attributes.Unit_ConversionFactorA} {UNITADD} .");
            _unitAdded = true;
        }

        private void AddUnitMult(StringBuilder stringBuilder)
        {
            if (!_unitAdded && !_selects.Contains(UNIT)) { AddUnit(stringBuilder); };
            stringBuilder.AppendLine($"{PATTERNTAB}{UNIT} {DDHUBPREFIX}{Attributes.Unit_ConversionFactorB} {UNITMULT} .");
            _unitMultAdded = true;
        }

        private void AddSignal(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"{PATTERNTAB}{DATAPOINT_VARIABLE} {DDHUBPREFIX}{Verbs.HasDynamicValue} {SIGNAL_VARIABLE} .");
            _signalAdded = true;
        }

        private void AddPatternItem(StringBuilder stringBuilder, (string sub, string verb, string obj) patternItem)
        {
            if (patternItem.verb == RDFTYPE || patternItem.verb == RDFSSUBCLASSOF)
            {
                stringBuilder.AppendLine($"{PATTERNTAB}{patternItem.sub} {patternItem.verb} {patternItem.obj} .");
            }
            else
            {
                stringBuilder.AppendLine($"{PATTERNTAB}{patternItem.sub} {DDHUBPREFIX}{patternItem.verb} {patternItem.obj} .");
            }
        }
        public static string GetResourcePatternItem(string resourceUri)
        {
            return $"<{resourceUri}>";
        }
        public static string GetResourcePatternItem(string resourceNameSpace, string resourceID)
        {
            return $"<{resourceNameSpace}{resourceID}>";
        }

        public static string GetClassResourceItem(string className)
        {
            return $"{DDHUBPREFIX}{className}";
        }

        private void GenerateSelectLine(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append("SELECT");
            foreach (var s in _selects)
            {
                stringBuilder.Append(" " + s);
            }
            stringBuilder.Append(" ");
        }
    }
}
