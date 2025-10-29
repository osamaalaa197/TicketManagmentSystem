using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;

namespace TicketManagementSystem.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler:IRequestHandler<DeleteCategoryCommand, DeleteCategoryCommandResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository=categoryRepository;
        }

        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteCategoryCommandResponse();
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null) {
                response.Success = false;
                response.Message = "Category not found";
                return response;
            }
            var result=await _categoryRepository.SoftDeleteAysncAsync(category);
            response.Success = result;
            return response;
        }
    }
}
