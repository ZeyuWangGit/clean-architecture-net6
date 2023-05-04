using GloboTicket.TicketManagement.Domain.Common;

namespace GloboTicket.TicketManagement.Domain.Entities;

public class Category: AuditableEntity
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public ICollection<Event> Events { get; set; }
}