namespace MarketPlace {
    public class Product {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class ShoppingCart {
        public List<Product> Products { get; }

        public ShoppingCart() {
            Products = new List<Product>();
        }
    }
}