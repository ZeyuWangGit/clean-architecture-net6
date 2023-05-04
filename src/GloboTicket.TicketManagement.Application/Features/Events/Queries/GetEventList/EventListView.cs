namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventList;

public class EventListView
{
    public Guid EventId { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string ImageUrl { get; set; }
}