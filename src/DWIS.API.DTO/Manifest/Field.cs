using System;

namespace DWIS.API.DTO 
{
    public class Field : IEquatable<Field>
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }

        public override bool Equals(object obj) => Equals(obj as Field);
        public override int GetHashCode() => (FieldName, FieldValue).GetHashCode();

        public bool Equals(Field other)
        {
            if (other is null)
                return false;

            return FieldName == other.FieldName
                && FieldValue == other.FieldValue;
        }
    }

}
