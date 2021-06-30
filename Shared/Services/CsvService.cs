using Domain.Entities;
using TinyCsvParser.Mapping;

namespace Shared.Services
{
    public class CsvService : CsvMapping<PriceData>
    {
        public CsvService() : base()
        {
            MapProperty(0, x => x.Date);
            MapProperty(1, x => x.OpeningPrice);
            MapProperty(2, x => x.ClosingPrice);
        }
    }
}
