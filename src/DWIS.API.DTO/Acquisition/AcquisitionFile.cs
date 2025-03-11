using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DWIS.API.DTO
{
    public class AcquisitionFile : IList<AcquisitionItem>
    {
        public AcquisitionItem this[int index] { get => Items[index]; set => Items[index] = value; }

        public IList<AcquisitionItem> Items { get; set; } = new List<AcquisitionItem>();

        public int Count => Items.Count;

        public bool IsReadOnly => Items.IsReadOnly;

        public static AcquisitionFile FromJsonString(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AcquisitionFile>(json);
        }
        public static string ToJsonString(AcquisitionFile file)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(file, Newtonsoft.Json.Formatting.Indented);
        }

        public void Add(AcquisitionItem item)
        {
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(AcquisitionItem item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(AcquisitionItem[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<AcquisitionItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public int IndexOf(AcquisitionItem item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, AcquisitionItem item)
        {
            Items.Insert(index, item);
        }

        public bool Remove(AcquisitionItem item)
        {
            return Items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Items).GetEnumerator();
        }
    }
}
