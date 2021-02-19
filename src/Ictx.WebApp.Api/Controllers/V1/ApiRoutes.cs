using Ictx.WebApp.Api.Helper;

namespace Ictx.WebApp.Api.Controllers.V1
{
    public static class ApiRoutesV1
    {
        public class DipendenteRoute 
        {
            public const string ControllerBase = ApiHelper.ApiRouteVersionedBase + "/Dipendente";
            public const string Get = ControllerBase;
            public const string GetById = ControllerBase+ "/{id}";
            public const string Post = ControllerBase;
            public const string Put = ControllerBase + "/{id}";
            public const string Delete = ControllerBase + "/{id}";
        }
    }
}
