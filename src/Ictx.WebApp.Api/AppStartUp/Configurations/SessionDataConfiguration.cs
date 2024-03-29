﻿using Ictx.WebApp.Api.Services;
using Ictx.WebApp.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Ictx.WebApp.Api.AppStartUp.Configurations;

public static class SessionDataConfiguration
{
	/// <summary>Configura la Dependency Injection aggiungendo i dati provenienti dal client, inserendoli in una classe apposita</summary>
	public static IServiceCollection ConfigureSessionData(this IServiceCollection services)
	{
		services.AddScoped<SessionDataService>();

		services.AddScoped<IUserData>(serviceProvider => {
			var sessionDataService = serviceProvider.GetService<SessionDataService>();
			return sessionDataService.GetSessionData();
		});

		return services;
	}
}