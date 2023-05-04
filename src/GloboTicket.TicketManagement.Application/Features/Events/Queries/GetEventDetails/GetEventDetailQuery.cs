using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetails;

public class GetEventDetailQuery : IRequest<EventDetailView>
{
    public Guid Id { get; set; }
}