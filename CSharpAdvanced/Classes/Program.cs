namespace Classes {
    class Program {
        static void Main(string[] args) {
            Car car1 = new Car();
            car1.Make = "Oldsmobile";
            car1.Model = "Cutlas Supreme";
            car1.Year = 1986;
            car1.Color = "Silver";
            
            Console.WriteLine("{0} {1} {2} {3}", car1.Make, car1.Model, car1.Year, car1.Color);
            Console.WriteLine("Market Value: "+car1.DetermineMarketValue());

            Car car2 = new Car("Ford", "Escape", 2005, "White");
            Console.WriteLine("{0} {1} {2} {3}", car2.Make, car2.Model, car2.Year, car2.Color);
            Console.WriteLine("Market Value: "+car2.DetermineMarketValue());
        }
        
    }

    class Car {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }

        public Car() {
            Make = "";
            Model = "";
            Year = 0;
            Color = "";
        }
        public Car(string make, string model, int year, string color) {
            Make = make;
            Model = model;
            Year = year;
            Color = color;
        }

        public decimal DetermineMarketValue() {
            decimal marketValue;
            if (Year > 1990)
                marketValue = 10000;
            else
                marketValue = 6000;
            return marketValue;
        }

    }
}
