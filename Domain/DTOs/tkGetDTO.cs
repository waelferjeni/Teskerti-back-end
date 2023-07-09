using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class tkGetDTO
    {
         public int Id { get; set; } 
        public string type { get; set; }
        public DateTime dateAchat { get; set; }
        public int EmployeeId { get; set; }
        public Employee employee { get; set; }
    }
}
