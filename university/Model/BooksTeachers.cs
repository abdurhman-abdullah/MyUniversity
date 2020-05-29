using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace university.Model
{
    public class BooksTeachers
    {
        public int BookId { get; set; }
        public Books Books { get; set; }
        public int TeacherId { get; set; }
        public Teachers Teachers { get; set; }
        public virtual ICollection<BooksTeachers> BookTeachers { get; set; }
    }
}
