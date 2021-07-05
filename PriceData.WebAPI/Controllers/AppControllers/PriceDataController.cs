using Application.Features.PriceData.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PriceData.WebApi.Controllers.AppControllers
{
    public class PriceDataController : BaseApiController
    {
        [HttpGet("pricedata")]
        public async Task<IActionResult> Get([FromQuery]string path) =>
            Ok(await Mediator.Send(new ParsePriceDataCommand { Path = path}));
    }
}
