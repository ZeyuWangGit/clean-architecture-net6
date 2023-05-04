using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoryList;

public class GetCategoriesListQuery : IRequest<List<CategoryListView>>
{
}