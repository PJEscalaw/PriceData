using System;

namespace Domain.Entities
{
    public class Result
    {
        public int Number { get; set; }
        public DateTime? Buy { get; set; }
        public DateTime? Sell { get; set; }
        public double? PercentGained { get; set; }
    }
}
