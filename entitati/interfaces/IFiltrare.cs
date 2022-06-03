
namespace entitati {
    public interface IFiltrare<T> where T: ProdusAbstract, IPackageable {
        public IEnumerable<T> Filtrare(List<T> collection, ICriteriu<T> crit);
    }
}