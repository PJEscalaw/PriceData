using MediatR;
using Persistence.Contexts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.DbService.PriceData.Queries
{
    public class GetPriceDataQuery : IRequest<IEnumerable<Domain.Entities.PriceData>>
    {
    }

    public class GetPriceDataQueryHandler : IRequestHandler<GetPriceDataQuery, IEnumerable<Domain.Entities.PriceData>>
    {
        public async Task<IEnumerable<Domain.Entities.PriceData>> Handle(GetPriceDataQuery request, CancellationToken cancellationToken)
        {
            using AppDbContext db = new();
            return await Task.FromResult(db.PriceData);
        }
    }
}
