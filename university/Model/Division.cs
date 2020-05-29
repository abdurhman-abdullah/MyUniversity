using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace university.Model
{
    public class Division
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public long DivisionNo { get; set; }

        public virtual Specialties Specialties { get; set; }
        public virtual Books Books { get; set; }
        public virtual Teachers Teachers { get; set; }
        public ICollection<DivisionStudent> DivisionStudents { get; set; }
    }
}
