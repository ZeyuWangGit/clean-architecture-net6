using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventList;

public class GetEventListQuery : IRequest<List<EventListView>>
{
}