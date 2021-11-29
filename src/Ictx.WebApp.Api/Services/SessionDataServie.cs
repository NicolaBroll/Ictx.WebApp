using Ictx.WebApp.Core.Domain.Utente;
using Microsoft.AspNetCore.Http;

namespace Ictx.WebApp.Api.Services;

public class SessionDataService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionDataService(IHttpContextAccessor httpContextAccessor)
	{
        this._httpContextAccessor = httpContextAccessor;
    }

	///// <summary>Legge i dati dalla sorgente corretta e li inserisce all'interno di un oggetto SessionData</summary>
	//public UserData GetSessionData()
	//{
	//	var claim = _httpContextAccessor.HttpContext.User.FindFirst("seac_paghe_user_user_name");

	//	if (claim != null)
	//	{
	//		var clientId = _httpContextAccessor.HttpContext.User.FindFirst("client_id").Value;
	//		return new UserData(new System.Guid(clientId));
	//	}		

	//	return new UserData(System.Guid.Empty);
	//}

	/// <summary>Legge i dati dalla sorgente corretta e li inserisce all'interno di un oggetto SessionData</summary>
	public UserData GetSessionData()
	{
		return new UserData(System.Guid.NewGuid());
	}
}