using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Exceptions;
using GloboTicket.TicketManagement.Application.Models.Mail;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IEventRepository _eventRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<CreateEventCommandHandler> _logger;

    public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, IEmailService emailService, ILogger<CreateEventCommandHandler> logger)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateEventCommandValidator(_eventRepository);
        var validationResults = await validator.ValidateAsync(request, cancellationToken);

        if (validationResults.Errors.Count > 0)
        {
            throw new ValidationException(validationResults);
        }

        var @event = _mapper.Map<Event>(request);
        @event = await _eventRepository.AddAsync(@event);

        var email = new Email() { To = "adamjwatkins@gmail.com", Body = $"A new event was created: {request}", Subject = "A new event was created" };

        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception ex)
        {
            //this shouldn't stop the API from doing anything else so this can be logged
            _logger.LogError($"Mailing about event {@event.EventId} failed due to an error with the mail service: {ex.Message}");
        }

        return @event.EventId;
    }
}