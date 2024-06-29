namespace ApiDemo.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
