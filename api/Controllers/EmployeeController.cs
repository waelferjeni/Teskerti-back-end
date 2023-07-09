using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.Commands;
using Domain.Queries;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EmployeeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            return _mediator.Send(new GetListQuery<Employee>(null, null))
                     .Result.Select(phase => _mapper.Map<EmployeeDTO>(phase));

        }

        [HttpGet("{id}")]
        public async Task<EmployeeDTO> GetEmployeeByID(int id)
        {
            var employee = _mediator.Send(new GetQuery<Employee>(condition: c => c.Id == id,includes:i=>i.Include(i=>i.Tickets))).Result;
            return _mapper.Map<EmployeeDTO>(employee);

        }

        [HttpPost("PostEmployee")]
        public async Task<string> PostEmployee(EmployeeDTO employeedto)
        {
            var employee = _mapper.Map<Employee>(employeedto);
            return await _mediator.Send(new PostCommand<Employee>(employee));
        }


        [HttpPut("PutEmployee")]
        public async Task<string> PutEmployee(Employee employee)
        {
            return await _mediator.Send(new PutCommand<Employee>(employee));
        }

        [HttpDelete("DeleteEmployee")]
        public async Task<string> DeleteEmployee(int id)
        {
            return await _mediator.Send(new DeleteCommand<Employee>(id));
        }
    }
}
