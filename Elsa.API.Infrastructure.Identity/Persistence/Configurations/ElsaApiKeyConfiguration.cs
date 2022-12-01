using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elsa.API.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация ключа авторизации.
/// </summary>
public class ElsaApiKeyConfiguration : IEntityTypeConfiguration<ElsaApiKey>
{
    /// <summary>
    /// Конфигурация.
    /// </summary>
    public void Configure(EntityTypeBuilder<ElsaApiKey> builder)
    {
        builder.ToTable("apiKeys");
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
