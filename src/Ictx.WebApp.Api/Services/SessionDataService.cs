using Ictx.WebApp.Api.Common;
using Ictx.WebApp.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Ictx.WebApp.Api.Services
{
    public class SessionDataService
	{
		//private readonly HttpContext _httpContext;

		//public SessionDataService(IHttpContextAccessor httpContextAccessor)
		//{
		//	this._httpContext = httpContextAccessor.HttpContext;
		//}

		/// <summary>Legge i dati dalla sorgente corretta e li inserisce all'interno di un oggetto SessionData</summary>
		public SessionData GetSessionData()
		{
			try
			{
				//var claim = this._httpContext.User.FindFirst("sub");

				//if (claim != null)
				return new SessionData(1);
			}
			catch { }

			return new SessionData(0);
		}
	}
}
