using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoryList;

public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, List<CategoryListView>>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesListQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<List<CategoryListView>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
    {
        var allCategories = (await _categoryRepository.ListAllAsync()).OrderBy(x => x.Name);
        return _mapper.Map<List<CategoryListView>>(allCategories);
    }
}