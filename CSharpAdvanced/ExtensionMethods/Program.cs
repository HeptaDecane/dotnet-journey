using MarketPlace;

namespace ExtensionMethods {
    class Program {
        static void Main(string[] args) {
            ShoppingCart shoppingCart = new ShoppingCart();
            
            shoppingCart.Products.Add(new Product() {
                Name = "Netflix UHD",
                Price = 8.99
            });
            shoppingCart.Products.Add(new Product() {
                Name = "Being Nice",
                Price = 0.00
            });
            shoppingCart.Products.Add(new Product() {
               Name = "X-box",
               Price = 47.37
            });

            Console.WriteLine($"cart total: {shoppingCart.GetTotal()}");
            
        }
    }

    // extending `ShoppingCart` with `GetTotal()`
    // *use public static
    public static class ShoppingCartExtension {
        public static double GetTotal(this ShoppingCart cart) {
            double total = 0;
            foreach (var product in cart.Products)
                total = total + product.Price;
            return total;
        }
    }
}