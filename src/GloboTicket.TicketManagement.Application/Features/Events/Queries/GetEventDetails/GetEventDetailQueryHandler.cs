using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetails;

public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailView>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Event> _eventRepository;
    private readonly IAsyncRepository<Category> _categoryRepository;

    public GetEventDetailQueryHandler(IAsyncRepository<Category> categoryRepository, IAsyncRepository<Event> eventRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<EventDetailView> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id);

        var eventDetailDto = _mapper.Map<EventDetailView>(@event);

        var category = await _categoryRepository.GetByIdAsync(@event.CategoryId);

        eventDetailDto.Category = _mapper.Map<CategoryDto>(category);

        return eventDetailDto;
    }
}