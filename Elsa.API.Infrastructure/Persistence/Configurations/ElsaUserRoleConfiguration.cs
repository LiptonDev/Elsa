using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elsa.API.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация доступа пользователя.
/// </summary>
public class ElsaUserRoleConfiguration : IEntityTypeConfiguration<ElsaUserRole>
{
    /// <summary>
    /// Конфигурация.
    /// </summary>
    public void Configure(EntityTypeBuilder<ElsaUserRole> builder)
    {
        builder.ToTable("userRoles");
    }
}
