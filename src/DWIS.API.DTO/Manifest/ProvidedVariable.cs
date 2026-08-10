using System;
using System.Linq;

namespace DWIS.API.DTO
{
    /// <summary>
    /// A variable that will be written by the client that triggers the manifest injection. 
    /// </summary>
    public class ProvidedVariable : IEquatable<ProvidedVariable>
    {
        /// <summary>
        /// The desired variable ID. The final ID (from the main server's address space) may differ. 
        /// </summary>
        public string VariableID { get; set; }

        /// <summary>
        /// The rank of the signal: scalars can have a rank of 0, one dimensional arrays of 1, 2-d tables of 2, etc...
        /// Corresponds to the number of indices required to access a single value. 
        /// </summary>
        public int Rank { get; set; } = 0;

        /// <summary>
        /// When working with arrays, provides the size of the array. For a 1-dimensional array, with n entries the Dimensions is (n). 
        /// For a n x m matrix, the dimension is (n, m). 
        /// </summary>
        public int[] Dimensions { get; set; } = null;

        /// <summary>
        /// The type of the underlying data. So far, the following strings are accepted: double, float, int, long, short, string, bool. 
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// The initial value of the variable. This is optional, and if not provided, the variable will be initialized to the default value of the corresponding data type.
        /// </summary>
        public string InitialValue { get; set; } = "";

        public bool Equals(ProvidedVariable other)
        {
            if (other is null)
                return false;

            if ((Dimensions == null && other.Dimensions != null) || (Dimensions != null && other.Dimensions == null))
                return false;

            if (!Dimensions.SequenceEqual(other.Dimensions)) return false;


            return VariableID == other.VariableID
                && DataType == other.DataType
                && Rank == other.Rank
                && InitialValue == other.InitialValue;
        }

        public bool EqualsApartFromInitialValue(ProvidedVariable other)
        {
            if (other is null)
                return false;
            if ((Dimensions == null && other.Dimensions != null) || (Dimensions != null && other.Dimensions == null))
                return false;
            if (!Dimensions.SequenceEqual(other.Dimensions)) return false;

            return VariableID == other.VariableID
                && DataType == other.DataType
                && Rank == other.Rank;
        }

        public class ProvidedVariableComparer : System.Collections.Generic.IEqualityComparer<ProvidedVariable>
        {
            public bool Equals(ProvidedVariable x, ProvidedVariable y)
            {
                if (x is null && y is null) return true;
                if (x is null || y is null) return false;
                return x.EqualsApartFromInitialValue(y);
            }
            public int GetHashCode(ProvidedVariable obj)
            {
                return (obj.VariableID, obj.DataType, obj.Rank).GetHashCode();
            }
        }
    }
}
