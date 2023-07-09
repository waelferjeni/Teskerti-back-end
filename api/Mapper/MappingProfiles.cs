using AutoMapper;
using Domain.DTOs;
using Domain.Models;

namespace api.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Ticket, TicketDTO>();
            CreateMap<TicketDTO, Ticket>();
            CreateMap<tkputDTO, Ticket>();
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<Ticket, tkGetDTO>();
            CreateMap<tkGetDTO, Ticket>();
        }
    }
}
