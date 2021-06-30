using System;

namespace Domain.Entities
{
    public class PriceData
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public double? OpeningPrice { get; set; }
        public double? ClosingPrice { get; set; }
    }
}
