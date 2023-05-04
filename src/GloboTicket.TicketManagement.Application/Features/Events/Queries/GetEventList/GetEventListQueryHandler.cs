using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventList;

public class GetEventListQueryHandler : IRequestHandler<GetEventListQuery, List<EventListView>>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Event> _eventRepository;

    public GetEventListQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    }

    public async Task<List<EventListView>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
    {
        var allEvents = await _eventRepository.ListAllAsync();

        var eventsDto = _mapper.Map<List<EventListView>>(allEvents.OrderBy(x => x.Date)).ToList();

        return eventsDto;
    }
}