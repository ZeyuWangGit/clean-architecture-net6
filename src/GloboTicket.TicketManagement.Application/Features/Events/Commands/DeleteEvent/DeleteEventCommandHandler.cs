using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.DeleteEvent;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    }

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventToDelete = await _eventRepository.GetByIdAsync(request.EventId);

        await _eventRepository.DeleteAsync(eventToDelete);
    }
}