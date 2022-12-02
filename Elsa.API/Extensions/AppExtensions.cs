namespace Elsa.API.Extensions;

public static class AppExtensions
{
    /// <summary>
    /// Использовать авторизацию.
    /// </summary>
    /// <param name="application"></param>
    public static void UseAuth(this IApplicationBuilder application)
    {
        application.UseAuthentication();
        application.UseAuthorization();
    }

    /// <summary>
    /// Использовать Swagger.
    /// </summary>
    /// <param name="application"></param>
    public static void UseSwaggerExtensions(this IApplicationBuilder application)
    {
        application.UseSwagger();
        application.UseSwaggerUI(x =>
        {
            x.SwaggerEndpoint("/swagger/v1/swagger.json", "Elsa.API");
        });
    }
}
