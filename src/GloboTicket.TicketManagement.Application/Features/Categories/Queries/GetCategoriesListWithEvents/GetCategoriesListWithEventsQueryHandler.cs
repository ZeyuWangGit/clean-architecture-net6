using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;

public class GetCategoriesListWithEventsQueryHandler : IRequestHandler<GetCategoriesListWithEventsQuery, List<CategoryEventListView>>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesListWithEventsQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }
    public async Task<List<CategoryEventListView>> Handle(GetCategoriesListWithEventsQuery request, CancellationToken cancellationToken)
    {
        var list = await _categoryRepository.GetCategoriesWithEvents(request.IncludeHistory);
        return _mapper.Map<List<CategoryEventListView>>(list);
    }
}