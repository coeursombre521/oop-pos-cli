using System;
using System.Collections;

namespace entitati {
    public class ProdusAbstractNonGen : CollectionBase {
        public ProdusAbstract? this[int index] {
            get => (ProdusAbstract) List[index]!;
            set => List[index] = (ProdusAbstract) value!;
        }

        public int Add(ProdusAbstract? pa) => List.Add(pa);
        public int IndexOf(ProdusAbstract? pa) => List.IndexOf(pa);
        public void Insert(int idx, ProdusAbstract? pa) => List.Insert(idx, pa);
        public void Remove(ProdusAbstract? pa) => List.Remove(pa);
        public new void RemoveAt(int idx) => List.RemoveAt(idx);
        public bool Contains(ProdusAbstract? pa) => List.Contains(pa);
        public void Sort() => this.InnerList.Sort();

        protected override void OnValidate(object value) {
            if (!typeof(ProdusAbstract).IsAssignableFrom(value.GetType())) {
                throw new ArgumentException("Obiectul nu este de tipul ProdusAbstract.");
            }
        }
    }
}