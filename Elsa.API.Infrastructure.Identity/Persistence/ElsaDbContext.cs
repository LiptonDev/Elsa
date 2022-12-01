﻿using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Elsa.API.Infrastructure.Persistence;

/// <summary>
/// Elsa Db context.
/// </summary>
public class ElsaDbContext : IdentityDbContext<ElsaUser,
                                               ElsaRole,
                                               string,
                                               IdentityUserClaim<string>,
                                               ElsaUserRole,
                                               IdentityUserLogin<string>,
                                               IdentityRoleClaim<string>,
                                               IdentityUserToken<string>>
{
    private readonly IDateTimeService dateTime;
    private readonly IDomainEventsService domainService;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="options"></param>
    public ElsaDbContext(DbContextOptions<ElsaDbContext> options, IDateTimeService dateTime, IDomainEventsService domainService) : base(options)
    {
        this.dateTime = dateTime;
        this.domainService = domainService;
    }

    /// <summary>
    /// Ключи для доступа к API.
    /// </summary>
    public DbSet<ElsaApiKey> ApiKeys { get; set; }

    /// <summary>
    /// Soft deleting.
    /// </summary>
    /// <returns></returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var item in ChangeTracker.Entries<BaseEntity>())
        {
            if (item.State == EntityState.Deleted)
            {
                item.State = EntityState.Modified;
                item.Entity.IsDeleted = true;
                item.Entity.DeleteDate = dateTime.UtcNow;
            }
            if (item.State == EntityState.Added)
            {
                item.Entity.CreateDate = dateTime.UtcNow;
            }
        }

        var save = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents();

        return save;
    }

    /// <summary>
    /// Опубликовать изменения сущностей.
    /// </summary>
    /// <returns></returns>
    private async Task DispatchEvents()
    {
        foreach (var item in ChangeTracker.Entries<IHasDomainEvents>().Select(x => x.Entity.DomainEvents).SelectMany(x => x).Where(x => !x.IsPublished))
        {
            item.IsPublished = true;
            await domainService.PublishAsync(item);
        }
    }

    /// <summary>
    /// Model creating.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<ElsaUserRole>().ToTable("userRoles");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("roleClaims");
        builder.Entity<IdentityUserClaim<string>>().ToTable("userClaims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("userLogins");
        builder.Entity<IdentityUserToken<string>>().ToTable("userTokens");
    }
}
