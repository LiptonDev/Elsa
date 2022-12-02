using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elsa.API.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация пользователя.
/// </summary>
public class ElsaUserConfiguration : IEntityTypeConfiguration<ElsaUser>
{
    /// <summary>
    /// Конфигурация.
    /// </summary>
    public void Configure(EntityTypeBuilder<ElsaUser> builder)
    {
        builder.ToTable("users");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasMany(x => x.UserRoles).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.ApiKeys).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}
