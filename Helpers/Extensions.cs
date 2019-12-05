using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    //general purpose extensions class
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message) //ovewrite response with this
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*"); //allow any origin
        }
    }
}