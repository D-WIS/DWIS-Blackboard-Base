using System;

namespace DWIS.API.DTO
{
    public class InjectionProvider : IEquatable<InjectionProvider>
    {
        public string Name { get; set; }
        public string Company { get; set; }

        public override bool Equals(object obj) => Equals(obj as InjectionProvider);
        public override int GetHashCode() => (Name, Company).GetHashCode();

        public bool Equals(InjectionProvider other)
        {
            if (other is null)
                return false;

            return Name == other.Name
                && Company == other.Company;
        }
    }

}
