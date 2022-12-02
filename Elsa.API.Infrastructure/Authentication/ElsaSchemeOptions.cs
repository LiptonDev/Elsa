using Microsoft.AspNetCore.Authentication;

namespace Elsa.API.Infrastructure.Authentication;

/// <summary>
/// Настройки для авторизации.
/// </summary>
public class ElsaSchemeOptions : AuthenticationSchemeOptions
{
    /// <summary>
    /// События.
    /// </summary>
    public new ElsaSchemeEvents Events
    {
        get { return (ElsaSchemeEvents)base.Events!; }
        set { base.Events = value; }
    }
}
