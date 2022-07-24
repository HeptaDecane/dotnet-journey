namespace Generics {
    
    class GenericList<T> {
        public void Add(T obj) {
            throw new NotImplementedException();
        }

        public T this[int index] {
            get { throw new NotImplementedException(); }
        }
    }

    class GenericDictionary<TKey, TValue> {
        public void Add(TKey key, TValue value) {
            throw new NotImplementedException();
        }
        
        public void Get(TKey key) {
            throw new NotImplementedException();
        }
    }

    class Utils<T> where T:IComparable {
        public static T Max(T a, T b){
           return a.CompareTo(b)>0 ? a:b;
        }
    }
    
    class Book {
        public string Isbn { get; set; }
        public string Title { get; set; }
    }
    
    class Program {
        static void Main(string[] args) {
            var numbers = new GenericList<int>();
            int num = 10;
            numbers.Add(num);

            var books = new GenericList<Book>();
            Book book = new Book { Isbn = "1707", Title = "C# Advanced" };
            books.Add(book);
        }
    }
}