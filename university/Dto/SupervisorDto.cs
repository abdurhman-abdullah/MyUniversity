using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace university.Controllers
{
    public class SupervisorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NameFamily { get; set; }
        public DateTime? DatePublished { get; set; }
    }
}
