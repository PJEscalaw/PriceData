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
        public async Task<bool> Handle(DeleteResultCommand request, CancellationToken cancellationToken)
        {
            using AppDbContext db = new();
            var result = db.Result.ToList();
            db.Result.RemoveRange(result);
            await db.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}
