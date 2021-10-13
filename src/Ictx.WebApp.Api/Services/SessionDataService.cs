using Ictx.WebApp.Application.Models;
using Microsoft.AspNetCore.Http;

namespace Ictx.WebApp.Api.Services
{
    public class SessionDataService
	{
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionDataService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>Legge i dati dalla sorgente corretta e li inserisce all'interno di un oggetto SessionData</summary>
        public SessionData GetSessionData()
		{
			try
			{
				var claim = this._httpContextAccessor.HttpContext.User.FindFirst("sub");

				if (claim != null)
                {
					return new SessionData(int.Parse(claim.Value));
                }
			}
			catch { }

			return new SessionData(0);
		}
	}
}
