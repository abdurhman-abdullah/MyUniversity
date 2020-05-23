using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace university.Model
{
    public class TheSections
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3 , ErrorMessage = "Enter 3 Letters")]
        public string Name { get; set; }

        public DateTime? DatePublished { get; set; }

        public virtual DepartmentDirectors DepartmentDirectors { get; set; }
    }
}
