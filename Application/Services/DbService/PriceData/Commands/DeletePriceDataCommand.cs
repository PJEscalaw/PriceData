using MediatR;
using Persistence.Contexts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.DbService.PriceData.Commands
{
    public class DeletePriceDataCommand : IRequest<bool>
    {
    }
    public class DeletePriceDataCommandHandler : IRequestHandler<DeletePriceDataCommand, bool>
    {
        public async Task<bool> Handle(DeletePriceDataCommand request, CancellationToken cancellationToken)
        {
            using AppDbContext db = new();
            var prices = db.PriceData.ToList();
            db.RemoveRange(prices);
            await db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
