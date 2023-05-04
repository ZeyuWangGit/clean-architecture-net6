using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler: IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }
    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var createCategoryCommandResponse = new CreateCategoryCommandResponse();

        var validator = new CreateCategoryCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            createCategoryCommandResponse.Success = false;
            createCategoryCommandResponse.ValidationErrors = new List<string>();

            foreach (var validationError in validationResult.Errors)
            {
                createCategoryCommandResponse.ValidationErrors.Add(validationError.ErrorMessage);
            }
        }

        if (createCategoryCommandResponse.Success)
        {
            var category = new Category { Name = request.Name };
            category = await _categoryRepository.AddAsync(category);
            createCategoryCommandResponse.Category = _mapper.Map<CreateCategoryDto>(category);
        }

        return createCategoryCommandResponse;
    }
}