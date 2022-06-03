using System;
using entitati;

namespace app1 {
    public abstract class FiltruCriterii<T> : IFiltrare<T> where T : ProdusAbstract, IPackageable {
        public virtual IEnumerable<T> Filtrare(List<T> collection, ICriteriu<T> crit) {
            foreach (var i in collection) {
                if (crit.IsIndeplinit(i)) {
                    yield return i;
                }
            }
        }
    }
}