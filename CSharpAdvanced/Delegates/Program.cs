namespace Delegates {
    // delegate signature
    public delegate void StringDelegate(string text);

    class Program {
        static void Main(string[] args) {
            StringDelegate printUppercase = PrintUppercase;
            
            // using delegates:
            printUppercase("Hello World!");
            // or
            printUppercase.Invoke("Hello World!");
            
            // passing as a function param
            Write("HeLlO wOrLd!", PrintLowercase);
        }

        static void Write(string text, StringDelegate printStyle) {
            printStyle(text);
        }
        
        static void PrintUppercase(string text) {
            Console.WriteLine(text.ToUpper());
        }
        
        static void PrintLowercase(string text) {
            Console.WriteLine(text.ToLower());
        }
    }
}