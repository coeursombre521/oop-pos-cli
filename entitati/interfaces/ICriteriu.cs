
namespace entitati {
    public interface ICriteriu<T> where T : ProdusAbstract, IPackageable {
        bool IsIndeplinit(T item);
    }
}