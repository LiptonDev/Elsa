using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elsa.API.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация роли.
/// </summary>
public class ElsaRoleConfiguration : IEntityTypeConfiguration<ElsaRole>
{
    /// <summary>
    /// Конфигурация.
    /// </summary>
    public void Configure(EntityTypeBuilder<ElsaRole> builder)
    {
        builder.ToTable("roles");
        builder.HasMany(x => x.UserRoles).WithOne(x => x.Role).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);
    }
}
