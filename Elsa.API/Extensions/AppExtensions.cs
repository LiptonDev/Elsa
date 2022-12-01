namespace Elsa.API.Extensions;

public static class AppExtensions
{
    public static void UseAuth(this IApplicationBuilder application)
    {
        application.UseAuthentication();
        application.UseAuthorization();
    }

    public static void UseSwaggerExtensions(this IApplicationBuilder application)
    {
        application.UseSwagger();
        application.UseSwaggerUI(x =>
        {
            x.SwaggerEndpoint("/swagger/v1/swagger.json", "Elsa.API");
        });
    }
}
