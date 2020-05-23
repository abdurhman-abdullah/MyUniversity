using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace university.Dto
{
    public class DepartmentDirectorsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NameFamily { get; set; }
        public DateTime? DatePublished { get; set; }
    }
}
