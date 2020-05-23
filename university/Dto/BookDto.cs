using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace university.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public DateTime? DatePublished { get; set; }
    }
}
