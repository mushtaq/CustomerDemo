namespace EscoService
{
    public class Esco
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int Size { get; set; }
        public List<Customer>? Customers { get; set; }
    }
}
