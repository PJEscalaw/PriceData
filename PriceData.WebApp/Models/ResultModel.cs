using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PriceData.WebApp.Models
{
    public class ResultModel
    {
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? Buy { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? Sell { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.##} %")]
        public double? PercentGained { get; set; }
    }
}