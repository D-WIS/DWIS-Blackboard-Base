

namespace DWIS.SPARQL.Utils
{
    public static class SparqlUtils
    {
        public static string GetUnitInfo(string resource)
        {
            return "    SELECT  ?convA ?convB \r\nWHERE {\r\n                <mySignal> <http://ddhub.no/HasUnitOfMeasure> ?unit .\t\t\t\t\r\n                ?unit <http://ddhub.no/ConversionFactorA> ?convA .\t\t\t\r\n                ?unit <http://ddhub.no/ConversionFactorB> ?convB .\t\t\r\n }".Replace("mySignal", resource);
        }
        public static string GetClassDataPointSparqlQuery(params string[] classes)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.AppendLine("SELECT ?dataPoint ");
            stringBuilder.AppendLine("  WHERE { ");
            foreach (var className in classes)
            {
                stringBuilder.AppendLine("		?dataPoint <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://ddhub.no/" + className + "> . ");
            }
            stringBuilder.AppendLine("        }");

            return stringBuilder.ToString();
        }

        public static string GetClassSignalSparqlQuery(params string[] classes)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.AppendLine("SELECT ?dataValue  ");
            stringBuilder.AppendLine("  WHERE { ");
            foreach (var className in classes)
            {
                stringBuilder.AppendLine("		?dataPoint <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://ddhub.no/" + className + "> . ");
            }
            stringBuilder.AppendLine("		?dataPoint <http://ddhub.no/HasDynamicValue> ?dataValue . ");
            stringBuilder.AppendLine("        }");
            return stringBuilder.ToString();
        }

        public static string GetClassPointAndSignalSparqlQuery(params string[] classes)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.AppendLine("SELECT ?dataPoint ?dataValue  ");
            stringBuilder.AppendLine("  WHERE { ");
            foreach (var className in classes)
            {
                stringBuilder.AppendLine("		?dataPoint <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://ddhub.no/" + className + "> . ");
            }
            stringBuilder.AppendLine("		?dataPoint <http://ddhub.no/HasDynamicValue> ?dataValue . ");
            stringBuilder.AppendLine("        }");
            return stringBuilder.ToString();
        }

        public static string GetSuccessorSparqlQuery(string startResource, string verb, params (string constraintVerb, string constraintResource, bool forward)[] constraints)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.AppendLine("SELECT ?dataValue  ");
            stringBuilder.AppendLine("  WHERE { ");
            stringBuilder.AppendLine("		<" + startResource + "> <" + verb + "> ?dataValue . ");
            GenerateConstraints(constraints, stringBuilder);
            stringBuilder.AppendLine("        }");
            return stringBuilder.ToString();
        }

        public static string GetSuccessorAndTypeTypeSparqlQuery(string startResource, string verb, params (string constraintVerb, string constraintResource, bool forward)[] constraints)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.AppendLine("SELECT ?dataValue ?dateType ");
            stringBuilder.AppendLine("  WHERE { ");
            stringBuilder.AppendLine("		<" + startResource + "> <" + verb + "> ?dataValue . ");
            stringBuilder.AppendLine("		?dataValue " + " <" + "http://www.w3.org/1999/02/22-rdf-syntax-ns#type" + "> ?dateType . ");

            GenerateConstraints(constraints, stringBuilder);
            stringBuilder.AppendLine("        }");
            return stringBuilder.ToString();
        }


        public static string GetTypesQuery(string startResource)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.AppendLine("SELECT ?dateType ");
            stringBuilder.AppendLine("  WHERE { ");
            stringBuilder.AppendLine("		<" + startResource + "> < " + "http://www.w3.org/1999/02/22-rdf-syntax-ns#type" + "> ?dateType . ");

            stringBuilder.AppendLine("        }");
            return stringBuilder.ToString();
        }
        public static void GenerateConstraints((string constraintVerb, string constraintResource, bool forward)[] constraints, System.Text.StringBuilder stringBuilder)
        {
            if (constraints != null)
            {
                for (int i = 0; i < constraints.Length; i++)
                {
                    if (constraints[i].forward)
                    {
                        stringBuilder.AppendLine("		?dataValue " + "<" + constraints[i].constraintVerb + "> <" + constraints[i].constraintResource + "> . ");
                    }
                    else
                    {
                        stringBuilder.AppendLine("		<" + constraints[i].constraintResource + "> <" + constraints[i].constraintVerb + "> ?dataValue . ");
                    }
                }
            }
        }

        public static string GetPredecessorSparqlQuery(string targetResource, string verb, params (string constraintVerb, string constraintResource, bool forward)[] constraints)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.AppendLine("SELECT ?dataValue  ");
            stringBuilder.AppendLine("  WHERE { ");
            stringBuilder.AppendLine("		?dataValue <" + verb + "> <" + targetResource + "> . ");

            GenerateConstraints(constraints, stringBuilder);


            stringBuilder.AppendLine("        }");
            return stringBuilder.ToString();
        }

        public static bool ParseDouble(string rdfResource, out double parsed)
        {
            if (!string.IsNullOrEmpty(rdfResource))
            {
                if (rdfResource.EndsWith(RDFSharp.Model.RDFVocabulary.XSD.DOUBLE.URI.AbsoluteUri))
                {
                    int idx = rdfResource.IndexOf('^');
                    if (idx >= 1)
                    {
                        parsed = GetDouble(rdfResource.Substring(0, idx));
                        return !double.IsNaN(parsed);
                    }
                }
                else //try anyway...
                {
                    int idx = rdfResource.IndexOf('^');
                    if (idx >= 1)
                    {
                        parsed = GetDouble(rdfResource.Substring(0, idx));
                        return !double.IsNaN(parsed);
                    }
                }

            }
            parsed = double.NaN;
            return false;
        }

        public static bool ParseDoubleArray(string rdfResource, out double[] parsed)
        {
            if (!string.IsNullOrEmpty(rdfResource))
            {
                if (rdfResource.StartsWith("["))
                {
                    int idx = rdfResource.IndexOf(']');
                    rdfResource = rdfResource.Substring(1, idx - 1);
                    var elems = rdfResource.Split('.');
                    parsed = new double[elems.Length];
                    for (int i = 0; i < elems.Length; i++)
                    {
                        if (double.TryParse(elems[i], out double temp)) { parsed[i] = temp; }
                    }
                    return true;
                }
                else
                {
                    parsed = null;
                    return false;
                }


            }
            parsed = null;
            return false;
        }

        public static double GetDouble(string value)
        {
            double result;

            // Try parsing in the current culture
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out result) &&
                // Then try in US english
                !double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result) &&
                // Then in neutral language
                !double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out result))
            {
                result = double.NaN;
            }
            return result;
        }
    }
}