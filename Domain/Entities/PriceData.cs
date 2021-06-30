namespace Domain.Entities
{
    public class PriceData
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public double OpeningPrice { get; set; }
        public double ClosingPrice { get; set; }
    }
}
