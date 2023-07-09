using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string firstName { get; set; }

        public string lastName { get; set; }
        public int grade { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
