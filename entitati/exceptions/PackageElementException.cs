
namespace entitati {
    public class PackageElementException : Exception {
        public PackageElementException() { }

        public PackageElementException(string message) : base(message) { }
        
        public PackageElementException(string message, Exception inner) : base(message, inner) { }
    }
}