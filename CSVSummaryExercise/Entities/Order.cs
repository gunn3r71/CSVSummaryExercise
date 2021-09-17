namespace CSVSummaryExercise.Entities
{
    public class Order
    {
        public Order(string productName, double price, int quantity)
        {
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }

        public string ProductName { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }
    }
}