using Ictx.WebApp.Core.Entities;
using System.Threading.Tasks;

namespace Ictx.WebApp.Infrastructure.Services.Interface
{
    public interface IFoglioPresenzaService
    {
        Task<FoglioPresenza> Get(int idDipendente, int anno, int mese);
    }
}
