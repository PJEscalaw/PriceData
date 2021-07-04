using Application.Services.DbService.PriceData.Queries;
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
        private readonly IMediator _mediator;

        public DeletePriceDataCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<bool> Handle(DeletePriceDataCommand request, CancellationToken cancellationToken)
        {
            var prices = await _mediator.Send(new GetPriceDataQuery());

            using AppDbContext db = new();
            db.RemoveRange(prices);
            await db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
