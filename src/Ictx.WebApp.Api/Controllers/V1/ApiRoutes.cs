namespace Ictx.WebApp.Api.Controllers.V1
{
    public static class ApiRoutes
    {
        private const string Base = "api/v{version:apiVersion}";

        public class DipendenteRoute 
        {
            public const string ControllerBase = Base + "/Dipendente";
            public const string Get = ControllerBase;
            public const string GetById = ControllerBase+ "/{id}";
            public const string Post = ControllerBase;
            public const string Put = ControllerBase + "/{id}";
            public const string Delete = ControllerBase + "/{id}";
        }
    }
}
