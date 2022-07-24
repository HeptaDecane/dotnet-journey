namespace Lambdas {
    class Program {
        static void Main(string[] args) {

            Action<int, char> printTriangle = (rows, ch) => {
                for(int i = 0; i < rows; i++) {
                    for (int j = 0; j <= i; j++) 
                        Console.Write(ch+" ");
                    Console.Write("\n");
                }
            };
            printTriangle(5,'*');
            
            Console.WriteLine();
                
            Func<int, int> square = num => num*num;
            Func<int, int> factorial = num=>{
                int res = 1;
                for (int i = 1; i <= num; i++)
                    res = res * i;
                return res;
            };

            Console.WriteLine("5^2 = {0}", square(5));
            Console.WriteLine("5! = {0}", factorial(5));

            Console.WriteLine();
            
            var books = new List<Book>() {
                new Book(){Title="title01", Price=5},
                new Book(){Title="title02", Price=17},
                new Book(){Title="title03", Price=7},
                new Book(){Title="title04", Price=12},
            };

            var cheapBooks = books.FindAll(book => book.Price<10);
            foreach (var book in cheapBooks) {
                Console.WriteLine(book.Title);
            }

        }
    }

    class Book {
        public string Title { get; set; }
        public int Price { get; set; }
    }
}