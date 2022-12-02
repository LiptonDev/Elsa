using Elsa.API.Infrastructure.Authentication.Contexts;

namespace Elsa.API.Infrastructure.Authentication;

/// <summary>
/// События, происходящие во время обработки авторизации.
/// </summary>
public class ElsaSchemeEvents
{
    /// <summary>
    /// Invoked if Authorization fails and results in a Forbidden response.
    /// </summary>
    public Func<ForbiddenContext, Task> OnForbidden { get; set; } = context => Task.CompletedTask;

    /// <summary>
    /// Invoked before a challenge is sent back to the caller.
    /// </summary>
    public Func<ChallengeContext, Task> OnChallenge { get; set; } = context => Task.CompletedTask;
}
