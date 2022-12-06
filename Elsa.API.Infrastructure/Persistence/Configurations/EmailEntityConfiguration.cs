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
    public void Configure(EntityTypeBuilder<EmailEntity> builder)
    {
        builder.ToTable("mails");
        builder.Ignore(x => x.ToAsEnumerable);
        builder.Ignore(x => x.Fail);
        builder.HasQueryFilter(x => x.Fails < 3 && !x.Sended);
    }
}
