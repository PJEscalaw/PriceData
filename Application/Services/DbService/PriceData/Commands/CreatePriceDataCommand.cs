using MediatR;
using Persistence.Contexts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.DbService.PriceData.Commands
{
    public class CreatePriceDataCommand : IRequest<bool>
    {
        public List<Domain.Entities.PriceData> PriceData { get; set; }
    }
    public class CreatePriceDataCommandHandler : IRequestHandler<CreatePriceDataCommand, bool>
    {
        public async Task<bool> Handle(CreatePriceDataCommand request, CancellationToken cancellationToken)
        {   
            using AppDbContext db = new();
            await db.PriceData.AddRangeAsync(request.PriceData, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}
