using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Ticket
    {
        public int Id { get; set; } 
        public string type { get; set; }
        
        public DateTime dateAchat { get; set; }
        public int EmployeeId { get; set; }
     
        public Employee Employee { get; set; }
    }
}
