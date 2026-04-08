using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Application.Features.Categories.Commands.CreateCategory;

namespace TicketManagementSystem.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandReponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly TicketManagementSystem.Application.Contract.Persistence.IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository,IMapper mapper, TicketManagementSystem.Application.Contract.Persistence.IUnitOfWork unitOfWork)
        {
            _categoryRepository=categoryRepository;
            _mapper=mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdateCategoryCommandReponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response=new UpdateCategoryCommandReponse();
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null) 
            {
                response.Success = false;
                response.Message = "Category not found";
                return response;
            }
            var validator = new UpdateCategoryCommandValidation();
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Any())
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validation.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
                return response;
            }
            category.Name = request.Name;
            var result= await _categoryRepository.UpdateAysnc(category);
            response.Success = true;
            response.CategoryDto = _mapper.Map<UpdateCategoryDto>(result);
            return response;
        }
    }
}
