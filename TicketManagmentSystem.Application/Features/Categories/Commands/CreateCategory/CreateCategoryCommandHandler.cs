using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Features.Categories.Commands.CreateCategory
{
    internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IAsyncRepository<Category> categoryRepository,IMapper mapper)
        {
            _categoryRepository=categoryRepository;
            _mapper=mapper;
        }
        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response=new CreateCategoryCommandResponse();
            var validator = new CreateCategoryCommandValidation();
            var validation=validator.Validate(request);
            if (validation.Errors.Any()) {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validation.Errors) {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
                return response;
            }
            var category= _mapper.Map<Category>(request);
            var result= await _categoryRepository.AddAysnc(category);
            response.Success = true;
            response.Category=_mapper.Map<CreateCategoryDto>(category);  
            return response;
        }
    }
}
