using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DWIS.OPCUA.Schemas
{
   public static partial class ReferencesIds
    {
        public static string[] GetAllReferences(bool includePrefix = true)
        {
            var fields = typeof(ReferencesIds).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where(f => f.FieldType == typeof(string));
            return fields.Select(f => (includePrefix ? "http://ddhub.no/" + f.Name : f.Name)).ToArray();
        }
    }
}
