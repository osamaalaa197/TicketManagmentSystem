using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Features.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, List<CategoryListVm>>
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryListQueryHandler(IAsyncRepository<Category> categoryRepository,IMapper mapper)
        {
            _categoryRepository=categoryRepository;
            _mapper=mapper;
        }
        public async Task<List<CategoryListVm>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categories = (await _categoryRepository.GetAllAsync()).Where(e=>!e.IsDeleted).OrderBy(e => e.Name).ToList();
            var categoryList=_mapper.Map<List<CategoryListVm>>(categories);
            return categoryList;
        }
    }
}
