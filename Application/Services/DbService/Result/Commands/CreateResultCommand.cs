using MediatR;
using Persistence.Contexts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.DbService.Result.Commands
{
    public class CreateResultCommand : IRequest<bool>
    {
        public List<Domain.Entities.Result> Result { get; set; }
    }
    public class CreateResultCommandHandler : IRequestHandler<CreateResultCommand, bool>
    {
        public async Task<bool> Handle(CreateResultCommand request, CancellationToken cancellationToken)
        {
            using AppDbContext db = new();
            await db.AddRangeAsync(request.Result, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
