using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagementSystem.Application.Features.Categories.Commands.CreateCategory;
using TicketManagementSystem.Application.Features.Categories.Commands.DeleteCategory;
using TicketManagementSystem.Application.Features.Categories.Commands.UpdateCategory;
using TicketManagementSystem.Application.Features.Categories.Queries.GetCategoriesWithEvent;
using TicketManagementSystem.Application.Features.Categories.Queries.GetCategoryList;

namespace TicketManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet("GetAllCategory")]
        public async Task<ActionResult< CategoryListVm>> GetAllCategory()
        {
            var dtos=await _mediator.Send(new GetCategoryListQuery());
            return Ok(dtos);    
        }
        [HttpGet("GetCategoryWithEvent")]
        public async Task<ActionResult<List<CategoryEventListVm>>> GetCategoryWithEvent(bool includeHisotry)
        {
            var dto=await _mediator.Send(new GetCategoriesWithEventQuery());
            return Ok(dto);
        }
        [HttpPost]
        public async Task<ActionResult<CreateCategoryCommandResponse>> AddCategory(CreateCategoryCommand model)
        {
            var res=await _mediator.Send(model);
            return Ok(res);
        }
        [HttpPut]
        public async Task<ActionResult<UpdateCategoryCommandReponse>> UpdateCategory(UpdateCategoryCommand mode)
        {
            var res=await _mediator.Send(mode);
            return Ok(res);
        }
        [HttpDelete]
        public async Task<ActionResult<DeleteCategoryCommandResponse>> DeleteCategory(DeleteCategoryCommand mode)
        {
            var res=await _mediator.Send(mode);
            return Ok(res);
        }
    }
}
