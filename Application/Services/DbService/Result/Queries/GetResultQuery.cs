using MediatR;
using Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.DbService.Result.Queries
{
    public class GetResultQuery : IRequest<IEnumerable<Domain.Entities.Result>>{}
    public class GetResultQueryHandler : IRequestHandler<GetResultQuery, IEnumerable<Domain.Entities.Result>>
    {
        public async Task<IEnumerable<Domain.Entities.Result>> Handle(GetResultQuery request, CancellationToken cancellationToken)
        {
            using AppDbContext db = new();
            return await Task.FromResult(db.Result.ToList());
        }
    }
}
