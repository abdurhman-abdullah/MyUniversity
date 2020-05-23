using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace university.Model
{
    public class BookTeacherStudent
    {
        public int teacherId { get; set; }
        public Teachers teachers { get; set; }

        public int bookId { get; set; }
        public Books books { get; set; }

        public int StudentId { get; set; }
        public Students student { get; set; }
    }
}
