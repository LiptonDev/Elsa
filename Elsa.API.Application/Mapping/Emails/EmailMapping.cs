using AutoMapper;
using Elsa.API.Application.UseCases.Emails.Commands.Create;
using Elsa.API.Domain.Entities;
using Elsa.Core.Models.Emails.Request;

namespace Elsa.API.Application.Mapping.Emails;

/// <summary>
/// Маппинг для почты.
/// </summary>
public class EmailMapping : Profile
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public EmailMapping()
    {
        CreateMap<AddEmailToQueueRequest, CreateEmailCommand>();
        CreateMap<CreateEmailCommand, EmailEntity>().ForMember(x => x.To, x => x.MapFrom(m => string.Join(",", m.Recipients)));
    }
}
