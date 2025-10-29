using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Application.Features.Categories.Queries.GetCategoryList;

namespace TicketManagementSystem.Application.Features.Categories.Queries.GetCategoriesWithEvent
{
    public class GetCategoriesWithEventQueryHandler : IRequestHandler<GetCategoriesWithEventQuery, List<CategoryEventListVm>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesWithEventQueryHandler(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository=categoryRepository;
            _mapper=mapper;
        }
        public async Task<List<CategoryEventListVm>> Handle(GetCategoriesWithEventQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetCategoryWithEvent(request.IncludeHistory);
            return _mapper.Map<List<CategoryEventListVm>>(result);
        }
    }
}
