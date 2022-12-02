using Elsa.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elsa.API.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация писем.
/// </summary>
public class EmailEntityConfiguration : IEntityTypeConfiguration<EmailEntity>
{
    /// <summary>
    /// Конфигурация.
    /// </summary>
    /// <param name="builder"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Configure(EntityTypeBuilder<EmailEntity> builder)
    {
        builder.ToTable("mails");
        builder.Ignore(x => x.ToAsEnumerable);
        builder.HasQueryFilter(x => !x.IsDeleted && x.Fails < 3);
    }
}
