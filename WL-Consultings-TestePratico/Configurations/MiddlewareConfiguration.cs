using WL_Consultings_TestePratico.Middlewares;

namespace WL_Consultings_TestePratico.Configurations
{
    public static class MiddlewareConfiguration
    {
        public static void AddCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }

}
