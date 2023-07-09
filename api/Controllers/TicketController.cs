using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.Commands;
using Domain.Queries;
using Domain.DTOs;
using Domain.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        public readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TicketController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("GetAllTickets")]
        public async Task<IEnumerable<tkGetDTO>> GetAllTickets()
        {
            
            return _mediator.Send(new GetListQuery<Ticket>(includes: i => i.Include(e => e.Employee))).Result.Select(phase => _mapper.Map<tkGetDTO>(phase));
        }
       
        [HttpGet("{id}")]
        public async Task<tkGetDTO> GetTicketByID(int id)
        {
            var ticket = await _mediator.Send(new GetQuery<Ticket>(condition: c => c.Id == id,includes: i =>i.Include(e =>e.Employee)));
            return _mapper.Map<tkGetDTO>(ticket);

        }

        [HttpPost("PostTicket")]
        public async Task<string> PostTicket(TicketDTO ticketdto)
        {
            var ticket = _mapper.Map<Ticket>(ticketdto);
            ticket.dateAchat=DateTime.Now;
            return await _mediator.Send(new PostCommand<Ticket>(ticket));
        }

        [HttpPut("PutTicket")]
        public async Task<string> PutTicket(tkputDTO ticketdto)
        {
            var ticket = _mapper.Map<Ticket>(ticketdto);
            ticket.dateAchat = DateTime.Now;
            return await _mediator.Send(new PutCommand<Ticket>(ticket));
        }

        [HttpDelete("DeleteTicket")]
        public async Task<string> DeleteTicket(int id)
        {
            return await _mediator.Send(new DeleteCommand<Ticket>(id));
        }
    }
}
