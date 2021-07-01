using Application.Features.PriceData.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PriceData.WebAPI.Controllers.AppControllers
{
    public class PriceDataController : BaseApiController
    {
        [HttpGet("pricedata")]
        public async Task<IActionResult> Get() => 
            Ok(await Mediator.Send(new ParsePriceDataCommand()));
    }
}
