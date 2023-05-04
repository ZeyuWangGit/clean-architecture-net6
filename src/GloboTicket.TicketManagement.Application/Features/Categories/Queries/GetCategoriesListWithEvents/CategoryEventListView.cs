﻿namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;

public class CategoryEventListView
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public ICollection<CategoryEventDto> Events { get; set; }
}