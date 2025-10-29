using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Features.Categories.Commands.CreateCategory;
using TicketManagementSystem.Application.Features.Categories.Commands.UpdateCategory;
using TicketManagementSystem.Application.Features.Categories.Queries.GetCategoriesWithEvent;
using TicketManagementSystem.Application.Features.Categories.Queries.GetCategoryList;
using TicketManagementSystem.Application.Features.Events.Commands.CreateEvent;
using TicketManagementSystem.Application.Features.Events.Commands.UpdateEvent;
using TicketManagementSystem.Application.Features.Events.Queries.GetEventDetails;
using TicketManagementSystem.Application.Features.Events.Queries.GetEventList;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventVm>().ReverseMap();
            CreateMap<Event,EventDetailVm>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Category,CategoryListVm>().ReverseMap();
            CreateMap<Category,CategoryEventListVm>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();
            //Add Event
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, CreateEventDto>().ReverseMap();
            //Update Event
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            //Add Category
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();

            //Update Category'
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        }
    }
}
