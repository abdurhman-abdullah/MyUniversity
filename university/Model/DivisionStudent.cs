using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace university.Model
{
    public class DivisionStudent
    {
        public int DivisionId { get; set; }
        public Division Division { get; set; }
        public int StudentId { get; set; }
        public Students  Students { get; set; }
        public virtual ICollection<DivisionStudent> DivisionStudents { get; set; }
    }
}
