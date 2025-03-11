using System;

namespace DWIS.API.DTO
{
    public class InjectedReference : IEquatable<InjectedReference>
    {
        public NodeIdentifier Subject { get; set; }
        public string VerbURI { get; set; }
        public NodeIdentifier Object { get; set; }

        public override bool Equals(object obj) => Equals(obj as InjectedReference);
        public override int GetHashCode() => (Subject, VerbURI, Object).GetHashCode();

        public bool Equals(InjectedReference other)
        {
            if (other is null)
                return false;

            if (VerbURI == other.VerbURI && VerbURI == other.VerbURI)
            {
                if (Subject != null && Subject.Equals(other.Subject) && Object != null && Object.Equals(other.Object))
                { return true; }
                if (Subject == null)
                {
                    if (other.Subject != null) 
                        return false;
                }
                else
                {
                    if (!Subject.Equals(other.Subject))
                        return false;
                }
                if (Object == null)
                {
                    if (other.Object != null)
                        return false;
                }
                else
                {
                    if (!Object.Equals(other.Object))
                        return false;
                }
                return true;
            }
            else return false;
        }


        public override string ToString()
        {
            return Subject.ToString() + " " + VerbURI + " " + Object.ToString();
        }
    }

}
