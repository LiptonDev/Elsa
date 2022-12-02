using Elsa.Core.Models.Common;
using Microsoft.AspNetCore.Identity;

namespace Elsa.API.Application.Extensions;

/// <summary>
/// Расширения для <see cref="ElsaIdentityError"/>.
/// </summary>
public static class ElsaIdentityErrorExtensions
{
    /// <summary>
    /// Преобразовать ошибки <see cref="IdentityError"/> в <see cref="ElsaIdentityError"/>.
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    public static List<ElsaIdentityError> ToElsaErrors(this IEnumerable<IdentityError> errors)
    {
        return errors.GroupBy(x => x.Code, x => x.Description).Select(x => new ElsaIdentityError { ErrorCode = x.Key, Errors = x.ToArray() }).ToList();
    }
}
