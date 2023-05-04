using AutoMapper;
using GloboTicket.TicketManagement.Application.Features.Categories.Commands.CreateCategory;
using GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoryList;
using GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent;
using GloboTicket.TicketManagement.Application.Features.Events.Commands.UpdateEvent;
using GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetails;
using GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventList;
using GloboTicket.TicketManagement.Domain.Entities;

namespace GloboTicket.TicketManagement.Application.Profiles;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Event, EventListView>().ReverseMap();
        CreateMap<Event, CreateEventCommand>().ReverseMap();
        CreateMap<Event, UpdateEventCommand>().ReverseMap();
        CreateMap<Event, EventDetailView>().ReverseMap();
        CreateMap<Event, CategoryEventDto>().ReverseMap();

        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryListView>();
        CreateMap<Category, CategoryEventListView>();
        CreateMap<Category, CreateCategoryCommand>();
        CreateMap<Category, CreateCategoryDto>();
    }
}