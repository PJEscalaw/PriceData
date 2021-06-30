using System.Threading.Tasks;

namespace Application.Services
{
    public interface IPriceDataService
    {
        Task ReadCsv();
    }
}
