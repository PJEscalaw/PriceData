using MediatR;
using Persistence.Contexts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace Application.Services.DbService.PriceData.Commands
{
    public class CreatePriceDataCommand : IRequest<bool>
    {
        public List<CsvMappingResult<Domain.Entities.PriceData>> PriceData { get; set; }
    }
    public class CreatePriceDataCommandHandler : IRequestHandler<CreatePriceDataCommand, bool>
    {
        public async Task<bool> Handle(CreatePriceDataCommand request, CancellationToken cancellationToken)
        {
            var prices = new List<Domain.Entities.PriceData>();
            foreach (var price in request.PriceData)
                prices.Add(price.Result);

            using AppDbContext db = new();
            await db.AddRangeAsync(prices, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}
