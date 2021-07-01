using Application.Services;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PriceData.Commands
{
    public class ParsePriceDataCommand : IRequest<IEnumerable<Result>>{}

    public class ParsePriceDataCommandHandler : IRequestHandler<ParsePriceDataCommand, IEnumerable<Result>>
    {
        private readonly IPriceDataService _priceDataService;

        public ParsePriceDataCommandHandler(IPriceDataService priceDataService)
        {
            _priceDataService = priceDataService;
        }
        public async Task<IEnumerable<Result>> Handle(ParsePriceDataCommand request, CancellationToken cancellationToken)
        {
            return await _priceDataService.ProcessPriceDataAsync();
        }
    }
}
