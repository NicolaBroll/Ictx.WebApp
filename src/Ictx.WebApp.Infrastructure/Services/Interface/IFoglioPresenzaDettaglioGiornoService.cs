using Ictx.WebApp.Core.Entities;
using System.Threading.Tasks;

namespace Ictx.WebApp.Infrastructure.Services.Interface
{
    public interface IFoglioPresenzaDettaglioGiornoService
    {
        Task Delete(int id);
    }
}
