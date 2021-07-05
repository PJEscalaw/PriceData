using Application.Services.DbService.Result.Queries;
using MediatR;
using Persistence.Contexts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.DbService.Result.Commands
{
    public class DeleteResultCommand : IRequest<bool>
    {
    }
    public class DeleteResultCommandHandler : IRequestHandler<DeleteResultCommand, bool>
    {
        private readonly IMediator _mediator;

        public DeleteResultCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<bool> Handle(DeleteResultCommand request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetResultQuery(), cancellationToken);
            using AppDbContext db = new();
            db.Result.RemoveRange(results);
            await db.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}
